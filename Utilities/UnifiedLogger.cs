using System;
using AutomationExcercise.Utilities;
using AventStack.ExtentReports;

namespace AutomationExcercise.Utilities
{
    public static class UnifiedLogger
    {
        // Log info messages to both log4net and ExtentReports
        public static void Info(string message)
        {
            Log4NetInfo(message);
            TryReport(r => r.Info(message));
        }

        // Log error messages to both log4net and ExtentReports
        public static void Error(string message)
        {
            Log4NetError(message);
            TryReport(r => r.Fail(message)); // Replace 'Error' with 'Fail'
        }

        // Log debug messages to both log4net and ExtentReports
        public static void Debug(string message)
        {
            Log4NetDebug(message);
            TryReport(r => r.Log(Status.Info, $"DEBUG: {message}")); // Use 'Status.Info' with a "DEBUG" prefix
        }

        // Log warning messages to both log4net and ExtentReports
        public static void Warn(string message)
        {
            Log4NetWarn(message);
            TryReport(r => r.Warning(message));
        }

        // --- Internal log4net wrappers (copy from your Logger class) ---
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(UnifiedLogger));
        static UnifiedLogger()
        {
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetExecutingAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
        }
        private static void Log4NetInfo(string message) => _log.Info(message);
        private static void Log4NetError(string message) => _log.Error(message);
        private static void Log4NetDebug(string message) => _log.Debug(message);
        private static void Log4NetWarn(string message) => _log.Warn(message);

        // --- ExtentReports logging ---
        private static void TryReport(Action<ExtentTest> logAction)
        {
            try
            {
                var stepTest = ReportManager.GetStepTest();
                if (stepTest != null)
                    logAction(stepTest);
            }
            catch
            {
                // Swallow exceptions to avoid breaking test flow if report node is missing
            }
        }
    }
}
