using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CompetitionTaskMars.Utilities;

IWebDriver driver = new ChromeDriver();
driver.Navigate().GoToUrl("https://localhost.com/");


{
    Wait.WaitToBeClickable(driver, "XPath", "//div[@class='right item']/a[@class='item']", 3);

    //Navigate to Sign In
    IWebElement signinButton = driver.FindElement(By.XPath("//div[@class='right item']/a[@class='item']"));
    signinButton.Click();

    //Entering email
    IWebElement emailTextbox = driver.FindElement(By.XPath("//input[@name='email']"));
    emailTextbox.SendKeys("h.prabhaharan@gmail.com");

    //Enter password
    IWebElement passwordTextbox = driver.FindElement(By.XPath("//input[@name='password']"));
    passwordTextbox.SendKeys("123456");

    //Click login
    IWebElement loginButton = driver.FindElement(By.XPath("///button[@class='fluid ui teal button']"));
    loginButton.Click();
    Thread.Sleep(2000);
}
