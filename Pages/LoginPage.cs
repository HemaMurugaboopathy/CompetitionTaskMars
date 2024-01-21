using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class LoginPage : CommonDriver
    {
        public void LoginActions()
        {
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"home\"]/div/div/div[1]/div/a", 3);

            //Navigate to Sign In
            IWebElement signinButton = driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
            signinButton.Click();

            //Entering email
            IWebElement emailTextbox = driver.FindElement(By.XPath("//input[@name='email']"));
            emailTextbox.SendKeys("h.prabhaharan@gmail.com");

            //Enter password
            IWebElement passwordTextbox = driver.FindElement(By.XPath("//input[@name='password']"));
            passwordTextbox.SendKeys("123456");

            //Click login
            IWebElement loginButton = driver.FindElement(By.XPath("//button[@class='fluid ui teal button']"));
            loginButton.Click();
        }
    }
}
