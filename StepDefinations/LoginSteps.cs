using System;
using AutomationExcercise.Pages;
using AutomationExcercise.Utilities;
using OpenQA.Selenium;
using Reqnroll;

namespace AutomationExcercise.StepDefinations
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginSteps(IWebDriver driver, LoginPage loginPage)
        {
            _driver = driver;
            _loginPage = loginPage;
        }

        [StepDefinition("user navigate to the login page")]
        public void GivenUserNavigateToTheLoginPage()
        {
            _loginPage.GoToLoginPage();
        }

        [StepDefinition("user enters valid credentials")]
        public void WhenUserEntersValidCredentials()
        {
            string str = _loginPage.EnterUserNameToGet("test970@gmail.com");
            UnifiedLogger.Info(str);
            _loginPage.EnterPassword("test@970");
        }

        [StepDefinition("user click the login button")]
        public void WhenUserClickTheLoginButton()
        {
            _loginPage.ClickLogin();
        }

        [StepDefinition("user should be logged in successfully")]
        public void ThenUserShouldBeLoggedInSuccessfully()
        {
            throw new PendingStepException();
        }

        [StepDefinition("user enter email {string} and password {string}")]
        public void WhenUserEnterEmailAndPassword(string username, string password)
        {
            _loginPage.EnterUserName(username);
            _loginPage.EnterPassword(password);
        }

        [StepDefinition("user should see error message {string}")]
        public void ThenUserShouldSeeErrorMessage(string message)
        {
            bool isErrorDisplayed = _loginPage.VerifyingErrorMessage(message);
            if (!isErrorDisplayed)
            {
                throw new Exception($"Expected error message '{message}' was not displayed.");
            }
        }

    }
}
