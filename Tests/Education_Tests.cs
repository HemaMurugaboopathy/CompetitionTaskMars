using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
        [Test]
        public void EducationTest()
        {
            educationPageObj.Add_Education();
        }
        [TearDown]
        public void EducationTearDown() 
        {
            driver.Quit();
        }

    }
}
