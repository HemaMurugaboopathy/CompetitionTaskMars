using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;

namespace CompetitionTaskMars.Pages
{
    public class LoginPage : CommonDriver
    {
        //Find Element by ID
        private IWebElement signinButton => driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
        private IWebElement emailTextbox => driver.FindElement(By.XPath("//input[@name='email']"));
        private IWebElement passwordTextbox => driver.FindElement(By.XPath("//input[@name='password']"));
        private IWebElement loginButton => driver.FindElement(By.XPath("//button[@class='fluid ui teal button']"));
        public void LoginActions()
        {     
             Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"home\"]/div/div/div[1]/div/a", 3);

            //Navigate to Sign In
            signinButton.Click();

            //Entering email
            emailTextbox.SendKeys("h.prabhaharan@gmail.com");

            //Enter password
            passwordTextbox.SendKeys("123456");

            //Click login
            loginButton.Click();
        }
    }
}
