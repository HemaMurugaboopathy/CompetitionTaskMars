using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RazorEngine;

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
        EducationData educationData;

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

        [Test, Order(1), Description("This test is deleting all entries before addind new data")]
        public void Delete_All()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[@data-tab='third']//i[@class='remove icon']", 8);
            }
            catch (WebDriverTimeoutException e)
            {
                return;
            }
            IReadOnlyCollection<IWebElement> deleteButtons = driver.FindElements(By.XPath("//div[@data-tab='third']//i[@class='remove icon']"));
            //Delete all records in the list
            foreach (IWebElement deleteButton in deleteButtons)
            {
                deleteButton.Click();
            }
        }
        [Test, Order(2), Description("This test is creating a new Education")]
        public void Add_Education()
        {
            // Read test data for the AddEducation test case
            List<EducationData> educationDataList = EducationDataHelper.ReadEducationData(@"addEducationData.json");

            // Iterate through test data and retrieve AddEducation test data
            foreach (EducationData educationData in educationDataList)
            {
                test = extent.CreateTest("AddEducation").Info("Test Started");
                educationPageObj.Add_Education(educationData);

                string actualMessage = educationPageObj.getMessage();
                Assert.That(actualMessage == "Education has been added", "Actual message and expected message do not match");

                //Access education configuration settings
                string newCollegeName = educationPageObj.getCollegeName(educationData.CollegeName);
                string newCountry = educationPageObj.getCountry(educationData.Country);
                string newTitle = educationPageObj.getTitle(educationData.Title);
                string newDegree = educationPageObj.getDegree(educationData.Degree);
                string newGraduationYear = educationPageObj.getGraduationYear(educationData.GraduationYear);

                Assert.That(newCollegeName == educationData.CollegeName, "Actual college name and expected college name do not match");
                Assert.That(newCountry == educationData.Country, "Actual country and expected country do not match");
                Assert.That(newTitle == educationData.Title, "Actual title and expected title do not match");
                Assert.That(newDegree == educationData.Degree, "Actual degree and expected degree do not match");
                Assert.That(newGraduationYear == educationData.GraduationYear, "Actual year and expected year name do not match");

                test.Log(Status.Info, "Add education started");
            }
        }

        [TestCase(1)]
        [Test, Order(3), Description("This test is editing an existing Education")]
        public void Edit_Education(int id)
        {
            // Read test data for the AddEducation test case
            EducationData existingEducationData = EducationDataHelper
                .ReadEducationData(@"addEducationData.json")
                .FirstOrDefault(x => x.Id == id);
            EducationData newEducationData = EducationDataHelper
                .ReadEducationData(@"editEducationData.json")
                .FirstOrDefault(x => x.Id == id);

            test = extent.CreateTest("EditEducation").Info("Test Started");
            educationPageObj.Edit_Education(existingEducationData, newEducationData);

            //string actualMessage = educationPageObj.getMessage();
            //Assert.That(actualMessage == "Education has been updated", "Actual message and expected message do not match");

            //Access education configuration settings
            string updatedCollegeName = educationPageObj.getCollegeName(newEducationData.CollegeName);
            string updatedCountry = educationPageObj.getCountry(newEducationData.Country);
            string updatedTitle = educationPageObj.getTitle(newEducationData.Title);
            string updatedDegree = educationPageObj.getDegree(newEducationData.Degree);
            string updatedGraduationYear = educationPageObj.getGraduationYear(newEducationData.GraduationYear);

            Assert.That(updatedCollegeName == newEducationData.CollegeName, "Actual college name and expected college name do not match");
            Assert.That(updatedCountry == newEducationData.Country, "Actual country and expected country do not match");
            Assert.That(updatedTitle == newEducationData.Title, "Actual title and expected title do not match");
            Assert.That(updatedDegree == newEducationData.Degree, "Actual degree and expected degree do not match");
            Assert.That(updatedGraduationYear == newEducationData.GraduationYear, "Actual year and expected year name do not match");
            test.Log(Status.Info, "Edit education started");
        }
        [Test, Order(4), Description("Check if user able to cancel education field while editing the field")]
        [TestCase(1)]
        public void Cancel_Education(int id)
        {
            test = extent.CreateTest("CancelEducation").Info("Test Started");
            //educationPageObj.Delete_Education(existingEducationData);

            string cancelEducation = educationPageObj.getCancel();
            Assert.That(string.IsNullOrEmpty(cancelEducation), Is.True, "Cancelled successfully");
        }


        [Test, Order(5), Description("This test is deleting an existing Education")]
        [TestCase (1)]
        public void Delete_Education(int id)
        {
            //Read test data from the DeleteEducation test case
            EducationData existingEducationData = EducationDataHelper
                .ReadEducationData(@"deleteEducationData.json")
                .FirstOrDefault(x => x.Id == id);

            test = extent.CreateTest("DeleteEducation").Info("Test Started");
            educationPageObj.Delete_Education(existingEducationData);

            //string actualMessage = educationPageObj.getMessage();
            //Assert.That(actualMessage == "Education has been deleted", "Actual message and expected message do not match");
        }

        [TearDown]
        public void EducationTearDown() 
        {
            driver.Quit();
        }
    }
}
