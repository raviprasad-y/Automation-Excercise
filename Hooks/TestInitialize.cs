using System;
using AutomationExcercise.Drivers;
using AutomationExcercise.Utilities;
using AutomationExercise.DriverManagers;
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
        private readonly FeatureContext _featureContext;
        private readonly IDriverManager _driverManager;
        public TestInitialize(IWebDriver driver, IDriverManager driverManager, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _driverManager = driverManager;
        }
        [BeforeTestRun]
        public static void BeforeTestRun() => ReportManager.CreateReport();

        [BeforeScenario]
        public void BeforeScenario()
        {
            ReportManager.CreateScenario(_featureContext.FeatureInfo.Title, _scenarioContext.ScenarioInfo.Title);

            var scenarioNode = ReportManager.GetScenarioTest();
            scenarioNode.AssignCategory(_featureContext.FeatureInfo.Title);
            scenarioNode.AssignAuthor("QA_Automation");
            scenarioNode.AssignCategory(string.Join(",", _scenarioContext.ScenarioInfo.Tags));

            scenarioNode.Info($"Tags: {string.Join(", ", _scenarioContext.ScenarioInfo.Tags)}");
            scenarioNode.Info($"Browser: {ConfigReader.Get("browser")}");
            _driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepInfo = _scenarioContext.StepContext.StepInfo;
            var stepNode = ReportManager.CreateStep(stepInfo.Text);

            string mode = ConfigReader.GetScreenshotMode();
            bool capture = mode == "Always" || (mode == "OnFailure" && _scenarioContext.TestError != null);
            string screenshotPath = capture ? ReportManager.CaptureScreenshot(_driver, _scenarioContext.ScenarioInfo.Title, stepInfo.Text) : null;

            try
            {
                if (_scenarioContext.TestError != null)
                {
                    stepNode.Fail(_scenarioContext.TestError.ToString());
                    if (screenshotPath != null) stepNode.AddScreenCaptureFromPath(screenshotPath);
                }
                else
                {
                    stepNode.Pass("Step passed");
                    if (screenshotPath != null) stepNode.AddScreenCaptureFromPath(screenshotPath);
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
                scenarioTest.Fail(_scenarioContext.TestError.Message);
            else
                scenarioTest.Pass("Scenario passed");

            _driverManager.QuitDriver();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportManager.GetExtent().AddTestRunnerLogs($"Test run ended at: {DateTime.Now}");
            ReportManager.FlushReport();
        }
    }
    
}

