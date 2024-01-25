using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Education_Tests : CommonDriver
    { 
        LoginPage loginPageObj = new LoginPage();
        ProfilePage profilePageObj = new ProfilePage();
        Education educationPageObj = new Education();
        public static ExtentTest test;
        public static ExtentReports extent;
        EducationData educationDataAddEducation;

        public Education_Tests()
        {
            loginPageObj = new LoginPage();
            profilePageObj = new ProfilePage();
            educationPageObj = new Education();
        }

        [SetUp]      
        public void LoginSetUp()
        {
            //Open Chrome browser
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5000/Home");

            //Login page object initialization and definition
            loginPageObj.LoginActions();

            //Education page object initialization and deifinition
            profilePageObj.GoToEducationPage();
        }

        [SetUp]
        public void CommonSetUp()
        {
            //Specify the path to the JSON file
            string jsonFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data\\educationData.json");
            educationDataAddEducation = EducationDataReader.ReadEducationData(jsonFilePath, "AddEducation");
        }

        [OneTimeSetUp]
        public void ExtentStart()
        {
            extent = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(@"D:\Hema\IndustryConnect\Internship\CompetitionTask\CompetitionTaskMars\ExtentReports\EducationReport.html");
            extent.AttachReporter(sparkReporter);
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            extent.Flush();
        }

        [Test]
        public void Add_Education()
        {
            test = extent.CreateTest("AddEducation").Info("Test Started");
            educationPageObj.Add_Education(educationDataAddEducation);
           
            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education has been added", "Actual message and expected message do not match");

            //Access education configuration settings
            string newCollegeName = educationPageObj.getCollegeName(educationDataAddEducation.CollegeName);
            string newCountry = educationPageObj.getCountry(educationDataAddEducation.Country);
            string newTitle = educationPageObj.getTitle(educationDataAddEducation.Title);
            string newDegree = educationPageObj.getDegree(educationDataAddEducation.Degree);
            string newGraduationYear = educationPageObj.getGraduationYear(educationDataAddEducation.GraduationYear);

            Assert.That(newCollegeName == educationDataAddEducation.CollegeName, "Actual college name and expected college name do not match");
            Assert.That(newCountry == educationDataAddEducation.Country, "Actual country and expected country do not match");
            Assert.That(newTitle == educationDataAddEducation.Title, "Actual title and expected title do not match");
            Assert.That(newDegree == educationDataAddEducation.Degree, "Actual degree and expected degree do not match");
            Assert.That(newGraduationYear == educationDataAddEducation.GraduationYear, "Actual year and expected year name do not match");

            test.Log(Status.Info, "Add education started");
        }

        [TearDown]
        public void EducationTearDown() 
        {
            driver.Quit();
        }
    }
}
