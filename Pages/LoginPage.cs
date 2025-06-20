using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationExcercise.Utilities;
using OpenQA.Selenium;

namespace AutomationExcercise.Pages
{
    public class LoginPage : BasePage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        //Locators
        private readonly By UserName = By.XPath("//input[@data-qa='login-email']");
        private readonly By Password = By.XPath("//input[@data-qa='login-password']");
        private readonly By LoginButton = By.XPath("//button[@data-qa='login-button']");
        private static By ErrorMessage(string message) => By.XPath($"//*[text()='{message}']");
        
        public void GoToLoginPage()
        {
            _driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));
        }

        public void EnterUserName(string username)
        {
            SendKeys(UserName, username);
        }
        
        public void EnterPassword(string password) 
        {
            SendKeys(Password, password);
        }
        public void ClickLogin()
        {
            Click(LoginButton);
        }
        public bool VerifyingErrorMessage(string message)
        {
            return IsDisplayed(ErrorMessage(message));
        }
    }
}
