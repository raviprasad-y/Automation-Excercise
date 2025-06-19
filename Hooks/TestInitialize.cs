using System;
using AutomationExcercise.Drivers;
using AutomationExcercise.Utilities;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;

namespace AutomationExcercise.Hooks
{
    [Binding]
    public class TestInitialize
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public TestInitialize(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            string featureTitle = featureContext.FeatureInfo.Title;
            ReportManager.CreateReport(featureTitle);
            ReportManager.CreateFeature(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string url = ConfigReader.Get("BaseUrl");
            _driver.Navigate().GoToUrl(url);
            ReportManager.CreateScenario(_scenarioContext.ScenarioInfo.Title);

            // Log scenario tags and metadata
            var tags = string.Join(", ", _scenarioContext.ScenarioInfo.Tags);
            ReportManager.GetScenarioTest().Info($"Tags: {tags}");
            ReportManager.GetScenarioTest().Info($"Browser: {ConfigReader.Get("browser")}");
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepInfo = _scenarioContext.StepContext.StepInfo;
            var stepNode = ReportManager.CreateStep(stepInfo.Text);

            string screenshotMode = ConfigReader.GetScreenshotMode();
            bool shouldCapture = screenshotMode == "Always" ||
                                 (screenshotMode == "OnFailure" && _scenarioContext.TestError != null);

            string screenshotPath = null;
            if (shouldCapture)
            {
                screenshotPath = ReportManager.CaptureScreenshot(_driver, _scenarioContext.ScenarioInfo.Title, stepInfo.Text);
            }

            try
            {
                if (_scenarioContext.TestError != null)
                {
                    if (screenshotPath != null)
                        stepNode.Fail("Step failed").AddScreenCaptureFromPath(screenshotPath);
                    else
                        stepNode.Fail("Step failed");
                }
                else
                {
                    if (screenshotPath != null)
                        stepNode.Pass("Step passed").AddScreenCaptureFromPath(screenshotPath);
                    else
                        stepNode.Pass("Step passed");
                }
            }
            catch (Exception ex)
            {
                UnifiedLogger.Error($"Error logging step: {ex.Message}");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var scenarioTest = ReportManager.GetScenarioTest();
            if (_scenarioContext.TestError != null)
            {
                scenarioTest.Fail(_scenarioContext.TestError.Message);
            }
            else
            {
                scenarioTest.Pass("Scenario passed");
            }

            DriverFactory.QuitDriver();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            ReportManager.FlushReport();
        }
    }
    
}

