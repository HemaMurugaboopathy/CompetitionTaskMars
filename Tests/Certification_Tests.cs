using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using NUnit.Framework;

namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Certification_Tests : CommonDriver
    {
        LoginPage loginPageobj = new LoginPage();
        ProfilePage profilePageobj = new ProfilePage();
        Certification certificationPageobj = new Certification();
        public static ExtentTest test;
        public static ExtentReports extent;
        public Certification_Tests()
        {
            loginPageobj = new LoginPage();
            profilePageobj = new ProfilePage();
            certificationPageobj = new Certification();
        }

        [SetUp]
        public void LoginSetUp()
        {
            //Open Chrome browser
            Initialize();

            //Login page object initialization and definition
            loginPageobj.LoginActions();

            //Education page object initialization and deifinition
            profilePageobj.GoToCertificationPage();
        }

        [OneTimeSetUp]
        public void ExtentStart()
        {
            // Create a new instance of ExtentReports to manage test reports
            extent = new ExtentReports();

            // Create a new ExtentSparkReporter to define the HTML report file path and configuration
            var sparkReporter = new ExtentSparkReporter(@"D:\Hema\IndustryConnect\Internship\CompetitionTask\CompetitionTaskMars\ExtentReports\CertificationReport.html");
            
            // Attach the ExtentSparkReporter to the ExtentReports instance for report generation
            extent.AttachReporter(sparkReporter);
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            // Flush the ExtentReports instance to finalize and write all information to the report files
            extent.Flush();
        }

        [Test, Order(1), Description("This is deleting all certificate before entering details")]
        public void Delete_All()
        {
            // Create a new test and log the test start information
            test = extent.CreateTest("DeleteAllCertification").Info("Test Started");
            certificationPageobj.Delete_All();

            // Log a pass status for the test and add a corresponding log message
            test.Log(Status.Pass, "Delete all certification passed");        
        }

        [TestCase(1)]
        [Test, Order(2), Description("This is creating a new certification")]     
        public void Add_Certification(int id)
        {
            // Create a new test and log the test start information
            test = extent.CreateTest("AddCertification").Info("Test Started");
            
            // Read certification data from the specified JSON file and retrieve the first item with a matching Id
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
                certificationPageobj.Add_Certification(certificationData);

            //Access certification configuration settings
            string newCertificate = certificationPageobj.getCertificate(certificationData.Certificate);
            string newCertifiedFrom = certificationPageobj.getCertifiedFrom(certificationData.CertifiedFrom);
            string newCertifiedYear = certificationPageobj.getertifiedYear(certificationData.CertifiedYear);

            Assert.That(newCertificate == certificationData.Certificate, "Actual certificate and expected certificate does not match");
            Assert.That(newCertifiedFrom == certificationData.CertifiedFrom, "Actual certificate and expected certificate does not match");
            Assert.That(newCertifiedYear == certificationData.CertifiedYear, "Actual certificate and expected certificate does not match");
        }

        [TestCase(2)]
        [Test, Order(3), Description("This is creating a new certification")]
        public void Add_CertificationSpecialChar(int id)
        {
            // Create a new test and log the test start information
            test = extent.CreateTest("AddCertificationSpecialChar").Info("Test Started");

            // Read certification data from the specified JSON file and retrieve the first item with a matching Id
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
            certificationPageobj.Add_Certification(certificationData);

            // Arrange
            string actualMessage = certificationPageobj.getMessage();
            string expectedMessagePattern = @".* has been added to your certification.*";

            // Perform the assertion using a regular expression
            Assert.That(actualMessage, Does.Match(expectedMessagePattern), $"Actual message '{actualMessage}' does not match the expected pattern '{expectedMessagePattern}'");

            // Log a pass status for the test and add a corresponding log message
            test.Log(Status.Fail, "Certification failed: ");

            CaptureScreenshot("SpecialCharactersFailed");
        }

        [TestCase(3)]
        [Test, Order(4), Description("This is creating a certification with empyt text box")]
        public void Add_CertificationEmptyTextbox(int id)
        {
            // Create a new test and log the test start information
            test = extent.CreateTest("AddCertificationEmptyTextBox").Info("Test Started");

            // Read certification data from the specified JSON file and retrieve the first item with a matching Id
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
            certificationPageobj.Add_Certification(certificationData);

            // Arrange
            string actualMessage = certificationPageobj.getMessage();
            string expectedMessage = "Please enter Certification Name, Certification From and Certification Year"; // Update this to the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Actual message '{actualMessage}' does not match expected message '{expectedMessage}'");

            // Log a pass status for the test and add a corresponding log message
            test.Log(Status.Fail, "Certification failed: ");
           
            CaptureScreenshot("CertificationEmptyTextBoxFailed");
        }

        [TestCase(4)]
        [Test, Order(5), Description("This is creating a new certification with more characters")]
        public void Add_CertificationMoreChar(int id)
        {
            // Create a new test and log the test start information
            test = extent.CreateTest("AddCertificationSpecialChar").Info("Test Started");

            // Read certification data from the specified JSON file and retrieve the first item with a matching Id
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
            certificationPageobj.Add_Certification(certificationData);

            // Arrange
            string actualMessage = certificationPageobj.getMessage();
            string expectedMessagePattern = @".* has been added to your certification.*";

            // Perform the assertion using a regular expression
            Assert.That(actualMessage, Does.Match(expectedMessagePattern), $"Actual message '{actualMessage}' does not match the expected pattern '{expectedMessagePattern}'");

            // Log a pass status for the test and add a corresponding log message
            test.Log(Status.Fail, "Certification failed: ");
            
            CaptureScreenshot("CertificationMoreCharactersFailed");
        }
        
        [Test, Order(6), Description("This is editing an existing certificate")]
        [TestCase(1)]
        public void Edit_Certification(int id)
        {
            // Read certification data from the specified JSON file and retrieve the first item with a matching Id
            CertificationData existingCertificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x=>x.Id == id);
            CertificationData newCertificationData = CertificationDataHelper
              .ReadCertificationData(@"editCertificationData.json")
            .FirstOrDefault(x => x.Id == id);

            // Create a new test and log the test start information
            test = extent.CreateTest("EditCertification").Info("Test Started");
            certificationPageobj.Edit_Certification(existingCertificationData, newCertificationData);

            //Access certification configuration settings
            string updatedCertificate = certificationPageobj.getCertificate(newCertificationData.Certificate);
            string updatedCertifiedFrom = certificationPageobj.getCertifiedFrom(newCertificationData.CertifiedFrom);
            string updatedCertifiedYear = certificationPageobj.getertifiedYear(newCertificationData.CertifiedYear);

            Assert.That(updatedCertificate == newCertificationData.Certificate, "Actual certificate and expected certificate does not match");
            Assert.That(updatedCertifiedFrom == newCertificationData.CertifiedFrom, "Actual certificate and expected certificate does not match");
            Assert.That(updatedCertifiedYear == newCertificationData.CertifiedYear, "Actual certificate and expected certificate does not match");
        }

        [Test, Order(7), Description("Cancel when editing")]
        [TestCase (1)]
        public void Cancel_Certification(int id)
        {
            // Create a new test and log the test start information
            test = extent.CreateTest("CancelCertification").Info("Test Started");
            
            string cancelCertification = certificationPageobj.getCancel();
            Assert.That(string.IsNullOrEmpty(cancelCertification), Is.True, "Cancelled successfully");

            // Log a pass status for the test and add a corresponding log message
            test.Log(Status.Fail, "Cancelled Successfully: ");
        }


        [Test, Order(8), Description("This is deleting an existing certificate")]
        [TestCase(1)]
        public void Delete_Certification(int id)
        {
            // Read certification data from the specified JSON file and retrieve the first item with a matching Id
            CertificationData existingCertificationData = CertificationDataHelper
               .ReadCertificationData(@"deleteCertificationData.json")
               .FirstOrDefault(x => x.Id == id);

            // Create a new test and log the test start information
            test = extent.CreateTest("DeleteCertification").Info("Test Started");
            certificationPageobj.Delete_Certification(existingCertificationData);

            // Log a pass status for the test and add a corresponding log message
            test.Log(Status.Info, "Delete certification started");
        }

        [TearDown]
        public void CertificationTearDown()
        {
            driver.Quit();
        }
    }
}
