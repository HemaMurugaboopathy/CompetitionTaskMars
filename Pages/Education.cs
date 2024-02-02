using CompetitionTaskMars.Data;
using CompetitionTaskMars.Utilities;
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
        private IWebElement updateButton => driver.FindElement(By.XPath("//input[@value=\"Update\"]"));
        private IWebElement cancelButton => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//input[@value='Cancel']"));

        public void Delete_All()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[@data-tab='third']//i[@class='remove icon']", 8);
            }
            catch (WebDriverTimeoutException e)
            {
                return;
            }
            IReadOnlyCollection<IWebElement> deleteButtons = driver.FindElements(By.XPath("//div[@data-tab='third']//i[@class='remove icon']"));
            //Delete all records in the list
            foreach (IWebElement deleteButton in deleteButtons)
            {
                deleteButton.Click();
            }
        }
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
        public void Edit_Education(EducationData existingEducationData, EducationData newEducationData)
        {
            Thread.Sleep(4000);
            string xPath = $@"//div[@data-tab='third']//tr[" +
            $"td[1]='{existingEducationData.Country}' and td[2]='{existingEducationData.CollegeName}'" +
            $" and td[3]='{existingEducationData.Title}' and td[4]='{existingEducationData.Degree}'" +
            $" and td[5]='{existingEducationData.GraduationYear}']/td[last()]/span[1]";

            //Click edit icon to edit an existing college
            IWebElement editIcon = driver.FindElement(By.XPath(xPath));
            editIcon.Click();

            //Enter College/University name
            collegeTextbox.Clear();
            collegeTextbox.SendKeys(newEducationData.CollegeName);

            //Select Country of college
            SelectElement selectCountryOption = new SelectElement(countryDropdown);
            selectCountryOption.SelectByValue(newEducationData.Country);

            //Select Title
            SelectElement selectTitleOption = new SelectElement(titleDropdown);
            selectTitleOption.SelectByValue(newEducationData.Title);

            //Enter Degree
            degreeTextbox.Clear();
            degreeTextbox.SendKeys(newEducationData.Degree);

            //Select Year of graduation
            SelectElement selectYearOption = new SelectElement(yearDropdown);
            selectYearOption.SelectByValue(newEducationData.GraduationYear);

            //Click Update Button
            updateButton.Click();
        }
        public string getCancel()
        {
            try
            {
               return cancelButton.Text;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public void Delete_Education(EducationData existingEducationData) 
        {
            //Delete an existing education
            string xPath = $@"//div[@data-tab='third']//tr[" +
           $"td[1]='{existingEducationData.Country}' and td[2]='{existingEducationData.CollegeName}'" +
           $" and td[3]='{existingEducationData.Title}' and td[4]='{existingEducationData.Degree}'" +
           $" and td[5]='{existingEducationData.GraduationYear}']/td[last()]/span[2]";
            IWebElement deleteIcon = driver.FindElement(By.XPath(xPath));
            deleteIcon.Click();
        }
    }
 }
