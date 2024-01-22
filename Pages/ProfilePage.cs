using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class ProfilePage: CommonDriver
    {
        public void GoToEducationPage()
        {
            Thread.Sleep(3000);
            //Naviage to education page
            IWebElement educationTab = driver.FindElement(By.XPath("//div[@class='ui top attached tabular menu']/a[@data-tab='third']"));
            educationTab.Click();
        }

        public void GoToCertificationPage()
        {
            //Navigate to certification page
            IWebElement certificationTab = driver.FindElement(By.XPath("//div[@class='ui top attached tabular menu']/a[@data-tab='fourth']"));
            certificationTab.Click();
        }
    }
}
