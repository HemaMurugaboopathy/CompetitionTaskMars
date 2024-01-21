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
    public class Education: CommonDriver
    {
        public void Add_Education() 
        {
            //Click Add New button 
            IWebElement addNewButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div"));
            addNewButton.Click();

            //Enter College/University name
            IWebElement collegeTextbox = driver.FindElement(By.XPath("//input[@placeholder='College/University Name']"));
            collegeTextbox.SendKeys("Lincoln University");

            //Select Country of college
            IWebElement countryDropdown = driver.FindElement(By.XPath("//select[@name='country']"));
            SelectElement selectCountryOption = new SelectElement(countryDropdown);
            selectCountryOption.SelectByValue("New Zealand");

            //Select Title
            IWebElement titleDropdown = driver.FindElement(By.XPath("//select[@name='title']"));
            SelectElement selectTitleOption = new SelectElement(titleDropdown);
            selectTitleOption.SelectByValue("M.A");

            //Enter Degree
            IWebElement degreeTextbox = driver.FindElement(By.XPath("//input[@placeholder='Degree']"));
            degreeTextbox.SendKeys("Master of Applied Computing");

            //Select Year of graduation
            IWebElement yearDropdown = driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
            SelectElement selectYearOption = new SelectElement(yearDropdown);
            selectYearOption.SelectByValue("2023");

            //Click Add button 
            IWebElement addButton = driver.FindElement(By.XPath("//input[@value='Add']"));
            addButton.Click();


        }
    }

 }
