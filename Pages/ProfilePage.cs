using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;

namespace CompetitionTaskMars.Pages
{
    public class ProfilePage: CommonDriver
    {
        //Find Element By ID
        private IWebElement educationTab => driver.FindElement(By.XPath("//div[@class='ui top attached tabular menu']/a[@data-tab='third']"));
        private IWebElement certificationTab => driver.FindElement(By.XPath("//div[@class='ui top attached tabular menu']/a[@data-tab='fourth']"));

        public void GoToEducationPage()
        {
            Wait.WaitToBeClickable(driver, "XPath", "//div[@class='ui top attached tabular menu']/a[@data-tab='third']", 8);
            //Naviage to education page      
            educationTab.Click();
        }

        public void GoToCertificationPage()
        {
            Wait.WaitToBeClickable(driver, "XPath", "//div[@class='ui top attached tabular menu']/a[@data-tab='fourth']", 8);
            //Navigate to certification page           
            certificationTab.Click();
        }
    }
}
