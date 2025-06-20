using System;
using AutomationExcercise.Utilities;
using AventStack.ExtentReports;

namespace AutomationExcercise.Utilities
{
    public static class UnifiedLogger
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(UnifiedLogger));

        static UnifiedLogger()
        {
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetExecutingAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
        }

        public static void Info(string message) => Log(message, Status.Info);
        public static void Error(string message) => Log(message, Status.Fail);
        public static void Debug(string message) => Log("[DEBUG] " + message, Status.Info);
        public static void Warn(string message) => Log("[WARN] " + message, Status.Warning);

        public static void Action(string message) => Log("[ACTION] " + message, Status.Info);
        public static void Assertion(string message) => Log("[ASSERT] " + message, Status.Info);
        public static void Input(string field, string value) => Log($"[INPUT] {field} = '{value}'", Status.Info);

        private static void Log(string message, Status status)
        {
            switch (status)
            {
                case Status.Info: _log.Info(message); break;
                case Status.Warning: _log.Warn(message); break;
                case Status.Fail: _log.Error(message); break;
            }

            try
            {
                var stepTest = ReportManager.GetStepTest();
                stepTest?.Log(status, message);
            }
            catch { /* suppress */ }
        }

    }
}
