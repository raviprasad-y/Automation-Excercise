using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationExcercise.Utilities;
using OpenQA.Selenium;

namespace AutomationExcercise.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement UserName => _driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
        private IWebElement Password => _driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        private IWebElement LoginButton => _driver.FindElement(By.XPath("//button[@data-qa='login-button']"));
        //private IWebElement ErrorMessage(string message) => _driver.FindElement(By.XPath("//*[text()='Your email or password is incorrect!']"));
        private IWebElement ErrorMessage(string message) => _driver.FindElement(By.XPath("//*[text()='"+ message+"']"));
        
        public void GoToLoginPage()
        {
            _driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));
        }

        public void EnterUserName(string username)
        {
            UserName.Clear();
            UserName.SendKeys(username);
        }
        public string EnterUserNameToGet(string username)
        {
            UserName.Clear();
            UserName.SendKeys(username);
            string val = UserName.GetAttribute("value");
            return val;
        }
        public void EnterPassword(string password) 
        {
            Password.Clear();
            Password.SendKeys(password);
        }
        public void ClickLogin()
        {
            LoginButton.Click();
        }
        public bool VerifyingErrorMessage(string message)
        {
            return ErrorMessage(message).Displayed;
        }
    }
}
