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
        
        public Certification_Tests()
        {
            loginPageobj = new LoginPage();
            profilePageobj = new ProfilePage();
            certificationPageobj = new Certification();
        }

        [SetUp]
        public void LoginSetUp()
        {
            Initialize();

            //Login page object initialization and definition
            loginPageobj.LoginActions();

            //Education page object initialization and deifinition
            profilePageobj.GoToCertificationPage();
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

        [Test, Order(1), Description("This is deleting all certificate before entering details")]
        public void Delete_All()
        {
            test = extent.CreateTest("DeleteAllCertification").Info("Test Started");
            certificationPageobj.Delete_All();
            test.Log(Status.Pass, "Delete all certification passed");        
        }

        [TestCase(1)]
        [Test, Order(2), Description("This is creating a new certification")]     
        public void Add_Certification(int id)
        {
            test = extent.CreateTest("AddCertification").Info("Test Started");
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
                certificationPageobj.Add_Certification(certificationData);

            //string actualMessage = certificationPageobj.getMessage();
            //Assert.That(actualMessage == "Certification has been added", "Actual message and expected message do not match");

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
            test = extent.CreateTest("AddCertificationSpecialChar").Info("Test Started");
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
            certificationPageobj.Add_Certification(certificationData);

            // Arrange
            string actualMessage = certificationPageobj.getMessage();
            string expectedMessagePattern = @".* has been added to your certification.*";

            // Perform the assertion using a regular expression
            Assert.That(actualMessage, Does.Match(expectedMessagePattern), $"Actual message '{actualMessage}' does not match the expected pattern '{expectedMessagePattern}'");
            
            test.Log(Status.Fail, "Certification failed: ");
            Console.WriteLine(actualMessage);
            CaptureScreenshot("SpecialCharactersFailed");
        }

        [TestCase(3)]
        [Test, Order(4), Description("This is creating a certification with empyt text box")]
        public void Add_CertificationEmptyTextbox(int id)
        {
            test = extent.CreateTest("AddCertificationEmptyTextBox").Info("Test Started");
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
            certificationPageobj.Add_Certification(certificationData);

            // Arrange
            string actualMessage = certificationPageobj.getMessage();
            string expectedMessage = "Please enter Certification Name, Certification From and Certification Year"; // Update this to the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Actual message '{actualMessage}' does not match expected message '{expectedMessage}'");

            test.Log(Status.Fail, "Certification failed: ");
            Console.WriteLine(actualMessage);
            CaptureScreenshot("CertificationEmptyTextBoxFailed");
        }

        [TestCase(4)]
        [Test, Order(5), Description("This is creating a new certification with more characters")]
        public void Add_CertificationMoreChar(int id)
        {
            test = extent.CreateTest("AddCertificationSpecialChar").Info("Test Started");
            CertificationData certificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x => x.Id == id);
            certificationPageobj.Add_Certification(certificationData);

            // Arrange
            string actualMessage = certificationPageobj.getMessage();
            string expectedMessagePattern = @".* has been added to your certification.*";

            // Perform the assertion using a regular expression
            Assert.That(actualMessage, Does.Match(expectedMessagePattern), $"Actual message '{actualMessage}' does not match the expected pattern '{expectedMessagePattern}'");

            test.Log(Status.Fail, "Certification failed: ");
            Console.WriteLine(actualMessage);
            CaptureScreenshot("CertificationMoreCharactersFailed");
        }
        
        [Test, Order(6), Description("This is editing an existing certificate")]
        [TestCase(1)]
        public void Edit_Certification(int id)
        {
            CertificationData existingCertificationData = CertificationDataHelper
                .ReadCertificationData(@"addCertificationData.json")
                .FirstOrDefault(x=>x.Id == id);
            CertificationData newCertificationData = CertificationDataHelper
              .ReadCertificationData(@"editCertificationData.json")
            .FirstOrDefault(x => x.Id == id);

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
            string cancelCertification = certificationPageobj.getCancel();
            Assert.That(string.IsNullOrEmpty(cancelCertification), Is.True, "Cancelled successfully");
        }

        [Test, Order(8), Description("This is deleting an existing certificate")]
        [TestCase(1)]
        public void Delete_Certification(int id)
        {
            CertificationData existingCertificationData = CertificationDataHelper
               .ReadCertificationData(@"deleteCertificationData.json")
               .FirstOrDefault(x => x.Id == id);

            certificationPageobj.Delete_Certification(existingCertificationData);
        }

        [TearDown]
        public void CertificationTearDown()
        {
            driver.Quit();
        }
    }
}
