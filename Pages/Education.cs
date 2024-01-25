﻿using CompetitionTaskMars.Data;
using CompetitionTaskMars.Utilities;
using DocumentFormat.OpenXml.Math;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace CompetitionTaskMars.Pages
{
    public class Education: CommonDriver
    {
        private IWebElement addNewButton => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div"));
        private IWebElement collegeTextbox => driver.FindElement(By.XPath("//input[@placeholder='College/University Name']"));
        private IWebElement countryDropdown => driver.FindElement(By.XPath("//select[@name='country']"));
        private IWebElement titleDropdown => driver.FindElement(By.XPath("//select[@name='title']"));
        private IWebElement degreeTextbox => driver.FindElement(By.XPath("//input[@placeholder='Degree']"));
        private IWebElement yearDropdown => driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
        private IWebElement addButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement SuccessMessage => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private Func<string, IWebElement> newCollegeName = CollegeName => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CollegeName}']"));
        private Func<string, IWebElement> newCountry = Country => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Country}']"));
        private Func<string, IWebElement> newTitle = Title => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Title}']"));
        private Func<string, IWebElement> newDegree = Degree => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Degree}']"));
        private Func<string, IWebElement> newGraduationYear = GraduationYear => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{GraduationYear}']"));

        public void Add_Education(EducationData educationData) 
         {
            //Click Add New button 
            addNewButton.Click();

            //Enter College/University name
            collegeTextbox.SendKeys(educationData.CollegeName);
            
            //Select Country of college
            SelectElement selectCountryOption = new SelectElement(countryDropdown);
            selectCountryOption.SelectByValue(educationData.Country);
          
            //Select Title
            SelectElement selectTitleOption = new SelectElement(titleDropdown);
            selectTitleOption.SelectByValue(educationData.Title);

            //Enter Degree
            degreeTextbox.SendKeys(educationData.Degree);

            //Select Year of graduation
            SelectElement selectYearOption = new SelectElement(yearDropdown);
            selectYearOption.SelectByValue(educationData.GraduationYear);

            Wait.WaitToBeClickable(driver, "XPath", "//input[@value='Add']", 8);

            //Click Add button 
            addButton.Click(); 
        }
        public string getMessage()
        {
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 8);
            //Get the text message after entering education details
            return SuccessMessage.Text;
            }
        public string getCollegeName(string CollegeName)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CollegeName}']", 8);
            return newCollegeName(CollegeName).Text;
        }

        public string getCountry(string Country)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Country}']", 8);
            return newCountry(Country).Text;
        }
      
        public string getTitle(string Title)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Title}']", 8);
            return newTitle(Title).Text;
        }
        public string getDegree(string Degree)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Degree}']", 8);
            return newDegree(Degree).Text;
        }
        public string getGraduationYear(string GraduationYear)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist(driver, "XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{GraduationYear}']", 8);
            return newGraduationYear(GraduationYear).Text;
        }

    }

 }
