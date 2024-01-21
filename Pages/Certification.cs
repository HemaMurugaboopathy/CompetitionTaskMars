using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class Certification: CommonDriver
    {
        public static void Add_Certification(IWebDriver driver) 
        {
            // Click Add New button 
            IWebElement addnewButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div"));
            addnewButton.Click();

            //Enter Certificate or Award
            IWebElement certificateTextbox = driver.FindElement(By.XPath("//input[@placeholder='Certificate or Award']"));
            certificateTextbox.SendKeys("Honour");

            //Enter Certified from
            IWebElement certifiedTextbox = driver.FindElement(By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']"));
            certifiedTextbox.SendKeys("Honour");

            //Select Year
            IWebElement certifiedyearDropdown = driver.FindElement(By.XPath("//select[@name='certificationYear']"));
            SelectElement selectCertifiedYearOption = new SelectElement(certifiedyearDropdown);
            selectCertifiedYearOption.SelectByValue("2021");

            //Click Add button 
            IWebElement AddCertificationButton = driver.FindElement(By.XPath("//input[@value='Add']"));
            AddCertificationButton.Click();
        } 
    }
}
