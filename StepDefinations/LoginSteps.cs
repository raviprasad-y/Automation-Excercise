using System;
using System.ComponentModel.DataAnnotations;
using AutomationExcercise.Models;
using AutomationExcercise.Pages;
using AutomationExcercise.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
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
            var credential = JsonDataReader.LoadJson<CredentialSet>("TestData/Credentials.json");
            var validCred = credential.validCredentials;
            _loginPage.EnterUserName(validCred.username);
            _loginPage.EnterPassword(validCred.password);
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

        [StepDefinition("user enters invalid credentials")]
        public void WhenUserEntersInvalidCredentials()
        {
            var creds = JsonDataReader.LoadJson<CredentialSet>("TestData/ICredentials.json");
            var invalidCreds = creds.inValidCredentials;
            _loginPage.EnterUserName(invalidCreds.username);
            _loginPage.EnterPassword(invalidCreds.password);
        }

        [StepDefinition("user should see error message displayed")]
        public void ThenUserShouldSeeErrorMessageDisplayed()
        {
            var errorMessage = JsonDataReader.LoadJson<CredentialSet>("TestData/Credentials.json");
            var invalidCreds = errorMessage.errorMessages;
            bool isErrorDisplayed = _loginPage.VerifyingErrorMessage(invalidCreds.loginErrorMessage);
            if (!isErrorDisplayed)
            {
                throw new Exception($"Expected error message '{invalidCreds.loginErrorMessage}' was not displayed.");
            }
        }
    }
}
