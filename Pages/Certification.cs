using CompetitionTaskMars.Data;
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
    public class Certification : CommonDriver
    {
        //Find element by ID
        private IWebElement addnewButton => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div"));
        private IWebElement certificateTextbox => driver.FindElement(By.XPath("//input[@placeholder='Certificate or Award']"));
        private IWebElement certifiedFromTextbox => driver.FindElement(By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']"));
        private IWebElement certifiedyearDropdown => driver.FindElement(By.XPath("//select[@name='certificationYear']"));
        private IWebElement AddCertificationButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private Func<string, IWebElement> newCertificate = Certificate => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Certificate}']"));
        private Func<string, IWebElement> newCertifiedFrom = CertifiedFrom => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CertifiedFrom}']"));
        private Func<string, IWebElement> newCertifiedYear = CertifiedYear => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CertifiedYear}']"));
        private IWebElement UpdateCertificationButton => driver.FindElement(By.XPath("//input[@value=\"Update\"]"));

        public void Add_Certification(CertificationData certificationData)
        {
            // Click Add New button 
            addnewButton.Click();

            //Enter Certificate or Award
            certificateTextbox.SendKeys(certificationData.Certificate);

            //Enter Certified from
            certifiedFromTextbox.SendKeys(certificationData.CertifiedFrom);

            //Select Year
            SelectElement selectCertifiedYearOption = new SelectElement(certifiedyearDropdown);
            selectCertifiedYearOption.SelectByValue(certificationData.CertifiedYear);

            //Click Add button 
            AddCertificationButton.Click();
        }
        public string getCertificate(string Certificate)
        {
            Thread.Sleep(3000);
            //Wait.WaitToExist(driver, "XPath", "//a[@data-tab='fourth']//td[text()='{Certificate}']", 8);
            return newCertificate(Certificate).Text;
        }
        public string getCertifiedFrom(string CertifiedFrom)
        {
            Thread.Sleep(3000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Certifications']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CertifiedFrom}']", 8);
            return newCertifiedFrom(CertifiedFrom).Text;
        }
        public string getertifiedYear(string CertifiedYear)
        {
            Thread.Sleep(3000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Certifications']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Year}']", 8);
            return newCertifiedYear(CertifiedYear).Text;
        }

        public void Edit_Certification(CertificationData existingCertificationData, CertificationData newCertificationData)
        {
            string xPath = $@"//div[@data-tab='fourth']//tr[" +
                $"td[1]='{existingCertificationData.Certificate}' and td[2]='{existingCertificationData.CertifiedFrom}'" +
                $" and td[3]='{existingCertificationData.CertifiedYear}']/td[last()]/span[1]";

            //Click edit icon to edit an existing certificate
            IWebElement editIcon = driver.FindElement(By.XPath(xPath));
            editIcon.Click();

            //Enter Certificate or Award
            certificateTextbox.Clear();
            certificateTextbox.SendKeys(newCertificationData.Certificate);

            //Enter Certified from
            certifiedFromTextbox.Clear();
            certifiedFromTextbox.SendKeys(newCertificationData.CertifiedFrom);

            //Select Year
            SelectElement selectCertifiedYearOption = new SelectElement(certifiedyearDropdown);
            selectCertifiedYearOption.SelectByValue(newCertificationData.CertifiedYear);

            //Click Add button 
            UpdateCertificationButton.Click();
        }
        public void Delete_Certification(CertificationData existingCertificationData)
        {
            string xPath = $@"//div[@data-tab='fourth']//tr[" +
               $"td[1]='{existingCertificationData.Certificate}' and td[2]='{existingCertificationData.CertifiedFrom}'" +
               $" and td[3]='{existingCertificationData.CertifiedYear}']/td[last()]/span[2]";
            //Click edit icon to edit an existing certificate
            IWebElement deleteIcon = driver.FindElement(By.XPath(xPath));
            deleteIcon.Click();

        }
    }
}
