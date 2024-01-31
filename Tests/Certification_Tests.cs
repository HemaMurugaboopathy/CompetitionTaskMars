using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Open Chrome browser
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5000/Home");

            //Login page object initialization and definition
            loginPageobj.LoginActions();

            //Education page object initialization and deifinition
            profilePageobj.GoToCertificationPage();
        }
        [Test, Order(1), Description("This is creating a new certification")]
        public void Add_Certification()
        {
            // Read test data for the AddEducation test case
            List<CertificationData> certificationDataList = CertificationDataHelper.ReadCertificationData(@"addCertificationData.json");

            // Iterate through test data and retrieve AddCertification test data
            foreach (CertificationData certificationData in certificationDataList)
            {
                certificationPageobj.Add_Certification(certificationData);

                //Access certification configuration settings
                string newCertificate = certificationPageobj.getCertificate(certificationData.Certificate);
                string newCertifiedFrom = certificationPageobj.getCertifiedFrom(certificationData.CertifiedFrom);
                string newCertifiedYear = certificationPageobj.getertifiedYear(certificationData.CertifiedYear);

                Assert.That(newCertificate == certificationData.Certificate, "Actual certificate and expected certificate does not match");
                Assert.That(newCertifiedFrom == certificationData.CertifiedFrom, "Actual certificate and expected certificate does not match");
                Assert.That(newCertifiedYear == certificationData.CertifiedYear, "Actual certificate and expected certificate does not match");
            }
        }
        [TestCase(1)]
        [Test, Order(2), Description("This is editing an existing certificate")]
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
        [Test, Order(3), Description("This is deleting an existing certificate")]
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
