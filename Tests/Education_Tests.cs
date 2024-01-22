using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack;
using AventStack.ExtentReports.Reporter;


namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Education_Tests : CommonDriver
    { 
        LoginPage loginPageObj = new LoginPage();
        ProfilePage profilePageObj = new ProfilePage();
        Education educationPageObj = new Education();

        [SetUp]
        public void EducationSetUp()
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
        private static ExtentTest test;
        public static ExtentReports extent = null;

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
        public void EducationTest()
        {
            educationPageObj.Add_Education();
            ExtentTest test = null;
            test = extent.CreateTest("EducationTest").Info("Test Started");
            test.Log(Status.Info, "Add education started");
            
        }
        [TearDown]
        public void EducationTearDown() 
        {
            driver.Quit();
        }

    }
}
