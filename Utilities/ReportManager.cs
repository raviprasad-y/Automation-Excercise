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

        public static void CleanOldReports(int daysOld = 5)
        {
            string reportRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "Reports");
            if (Directory.Exists(reportRoot))
            {
                foreach (var dir in Directory.GetDirectories(reportRoot))
                {
                    if (Directory.GetCreationTime(dir) < DateTime.Now.AddDays(-daysOld))
                        Directory.Delete(dir, true);
                }
            }
        }
        public static ExtentReports CreateReport()
        {
            if (_extent == null)
            {
                CleanOldReports();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                string folderPath = Path.Combine(projectRoot, "Reports");
                Directory.CreateDirectory(folderPath);
                string reportPath = Path.Combine(folderPath, $"Report_{timestamp}.html");

                _sparkReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);

                _extent.AddSystemInfo("Environment", ConfigReader.Get("Environment"));
                _extent.AddSystemInfo("Browser", ConfigReader.Get("browser"));

                _extent.AddSystemInfo("Machine", Environment.MachineName);
                _extent.AddSystemInfo("User", Environment.UserName);
                _extent.AddSystemInfo("Build", Environment.GetEnvironmentVariable("BUILD_ID") ?? "N/A");
                _extent.AddSystemInfo("Branch", Environment.GetEnvironmentVariable("GIT_BRANCH") ?? "main");
                _extent.AddSystemInfo("OS", Environment.OSVersion.ToString());

                UnifiedLogger.Info($"Extent Report initialized at: {reportPath}");
            }
            return _extent;
        }

        public static ExtentReports GetExtent() => _extent;

        public static void CreateFeature(string featureTitle)
        {
            _featureTest = _extent.CreateTest(featureTitle);
        }

        public static void CreateScenario(string featureTitle, string scenarioTitle)
        {
            string fullTitle = $"{featureTitle} - {scenarioTitle}";
            _scenarioTest.Value = _extent.CreateTest(fullTitle)
                .AssignAuthor("QA_Automation")
                .AssignCategory(featureTitle); // or additional tags
        }

        public static ExtentTest GetScenarioTest() => _scenarioTest.Value;

        public static ExtentTest CreateStep(string stepText)
        {
            _stepTest.Value = _scenarioTest.Value.CreateNode(stepText);
            return _stepTest.Value;
        }

        public static ExtentTest GetStepTest() => _stepTest.Value;

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
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                string screenshotsDir = Path.Combine(projectRoot, "Reports", "Screenshots", safeScenario);
                Directory.CreateDirectory(screenshotsDir);

                string fileName = $"{safeStep}_{DateTime.Now:yyyyMMdd_HHmmss}.{format}";
                string filePath = Path.Combine(screenshotsDir, fileName);
                string relativePath = Path.Combine("Screenshots", safeScenario, fileName);

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);
                return relativePath;
            
            }
            catch (Exception ex)
            {
                UnifiedLogger.Error($"Screenshot capture failed: {ex.Message}");
                return null;
            }
        }
    }
}
