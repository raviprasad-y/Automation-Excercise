using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using Reqnroll;

namespace AutomationExcercise.Utilities
{
    public static class ReportManager
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter;
        private static ExtentTest _featureTest;
        private static AsyncLocal<ExtentTest> _scenarioTest = new();
        private static AsyncLocal<ExtentTest> _stepTest = new();

        public static ExtentReports CreateReport(string featureTitle)
        {
            if (_extent == null)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                string folderPath = Path.Combine(projectRoot, "Reports", featureTitle);
                Directory.CreateDirectory(folderPath);
                string reportPath = Path.Combine(folderPath, $"Report_{timestamp}.html");
                _sparkReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
                UnifiedLogger.Info($"Extent Report initialized at: {reportPath}");
            }
            return _extent;
        }

        public static void CreateFeature(string featureTitle)
        {
            if (_extent == null)
                throw new InvalidOperationException("Extent report is not initialized. Call CreateReport first.");
            _featureTest = _extent.CreateTest(featureTitle);
        }

        public static void CreateScenario(string scenarioTitle)
        {
            if (_featureTest == null)
                throw new InvalidOperationException("Feature node is not initialized. Call CreateFeature first.");
            _scenarioTest.Value = _featureTest.CreateNode(scenarioTitle);
        }

        public static ExtentTest GetScenarioTest()
        {
            if (_scenarioTest.Value == null)
                throw new InvalidOperationException("Scenario node is not initialized. Call CreateScenario first.");
            return _scenarioTest.Value;
        }

        public static ExtentTest CreateStep(string stepText)
        {
            if (_scenarioTest.Value == null)
                throw new InvalidOperationException("Scenario node is not initialized. Call CreateScenario first.");
            _stepTest.Value = _scenarioTest.Value.CreateNode(stepText);
            return _stepTest.Value;
        }

        public static ExtentTest GetStepTest()
        {
            if (_stepTest.Value == null)
                throw new InvalidOperationException("Step node is not initialized. Call CreateStep first.");
            return _stepTest.Value;
        }

        public static void FlushReport()
        {
            _extent?.Flush();
        }

        public static string CaptureScreenshot(IWebDriver driver, string scenarioTitle, string stepText, string format = "png")
        {
            try
            {
                string safeScenario = string.Join("_", scenarioTitle.Split(Path.GetInvalidFileNameChars()));
                string safeStep = string.Join("_", stepText.Split(Path.GetInvalidFileNameChars()));
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                string screenshotsDir = Path.Combine(projectRoot, "Reports", "Screenshots", safeScenario);
                Directory.CreateDirectory(screenshotsDir);

                string fileName = $"{safeStep}_{DateTime.Now:yyyyMMdd_HHmmss}.{format}";
                string filePath = Path.Combine(screenshotsDir, fileName);

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);
                return filePath;
            }
            catch (Exception ex)
            {
                UnifiedLogger.Error($"Screenshot capture failed: {ex.Message}");
                return null;
            }
        }
    }
}
