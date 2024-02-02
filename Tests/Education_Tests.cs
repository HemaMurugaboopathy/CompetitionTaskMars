using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using NUnit.Framework;

namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Education_Tests : CommonDriver
    {
        LoginPage loginPageObj = new LoginPage();
        ProfilePage profilePageObj = new ProfilePage();
        Education educationPageObj = new Education();

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
            Initialize();

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
            test = extent.CreateTest("DeleteAllEducation").Info("Test Started");
            educationPageObj.Delete_All();
            test.Log(Status.Pass, "Delete all education passed");
        }
    
        [Test, Order(2), Description("This test is creating a new Education")]
        [TestCase(1)]
        public void Add_Education(int id)
         {
                test = extent.CreateTest("AddEducation").Info("Test Started");
                EducationData educationData = EducationDataHelper
                .ReadEducationData(@"addEducationData.json")
                .FirstOrDefault(x => x.Id == id);
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

        [Test, Order(3), Description("This test is adding a new Education with special characters")]
        [TestCase(2)]
        public void Add_EducationSpecial(int id)
        {
            CaptureScreenshot("ScreenshotName");

            test = extent.CreateTest("AddEducationWithSpecialCharacters").Info("Test Started");
            EducationData educationData = EducationDataHelper
            .ReadEducationData(@"addEducationData.json")
            .FirstOrDefault(x => x.Id == id);
            educationPageObj.Add_Education(educationData);
            
             // Arrange
            string actualMessage = educationPageObj.getMessage();
            string expectedMessage = "Education has been added"; // Update this to the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Actual message '{actualMessage}' does not match expected message '{expectedMessage}'");

            test.Log(Status.Fail, "Education failed: ");
            Console.WriteLine(actualMessage);
            CaptureScreenshot("SpecialCharactersFailed");
        }

        [Test, Order(4), Description("This test is adding a new Education with empty text box")]
        [TestCase(3)]
        public void Add_EducationEmptyTextbox(int id)
        {
            CaptureScreenshot("ScreenshotName");
            test = extent.CreateTest("AddEducationWithSpecialCharacters").Info("Test Started");
            EducationData educationData = EducationDataHelper
            .ReadEducationData(@"addEducationData.json")
            .FirstOrDefault(x => x.Id == id);
            educationPageObj.Add_Education(educationData);

            // Arrange
            string actualMessage = educationPageObj.getMessage();
            string expectedMessage = "Please enter all the fields"; // Update this to the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Actual message '{actualMessage}' does not match expected message '{expectedMessage}'");

            test.Log(Status.Fail, "Education failed: ");
            Console.WriteLine(actualMessage);
            CaptureScreenshot("EmptyTextBoxFailed");
        }

        [Test, Order(5), Description("This test is adding a new Education with special characters")]
        [TestCase(4)]
        public void Add_EducationMoreCharacters(int id)
        {
            CaptureScreenshot("ScreenshotName");

            test = extent.CreateTest("AddEducationWithSpecialCharacters").Info("Test Started");
            EducationData educationData = EducationDataHelper
            .ReadEducationData(@"addEducationData.json")
            .FirstOrDefault(x => x.Id == id);
            educationPageObj.Add_Education(educationData);

            // Arrange
            string actualMessage = educationPageObj.getMessage();
            string expectedMessage = "Education has been added"; // Update this to the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Actual message '{actualMessage}' does not match expected message '{expectedMessage}'");

            test.Log(Status.Fail, "Education failed: ");
            Console.WriteLine(actualMessage);
            CaptureScreenshot("MoreCharactersFailed");
        }

        [TestCase(1)]
        [Test, Order(6), Description("This test is editing an existing Education")]
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

            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education as been updated", "Actual message and expected message do not match");

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

        [Test, Order(7), Description("Check if user able to cancel education field while editing the field")]
        [TestCase(1)]
        public void Cancel_Education(int id)
        {
            test = extent.CreateTest("CancelEducation").Info("Test Started");
            //educationPageObj.Delete_Education(existingEducationData);

            string cancelEducation = educationPageObj.getCancel();
            Assert.That(string.IsNullOrEmpty(cancelEducation), Is.True, "Cancelled successfully");
        }

        [Test, Order(8), Description("This test is deleting an existing Education")]
        [TestCase (1)]
        public void Delete_Education(int id)
        {
            //Read test data from the DeleteEducation test case
            EducationData existingEducationData = EducationDataHelper
                .ReadEducationData(@"deleteEducationData.json")
                .FirstOrDefault(x => x.Id == id);

            test = extent.CreateTest("DeleteEducation").Info("Test Started");
            educationPageObj.Delete_Education(existingEducationData);
        }

        [TearDown]
        public void EducationTearDown() 
        {
            Close();
        }
    }
}
