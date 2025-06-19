using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutomationExcercise.Utilities
{
    public static class ConfigReader
    {
        private static IConfigurationRoot _configuration;

        static ConfigReader()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var configFile = Path.Combine(basePath, "appsettings.json");

            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException($"Configuration file not found: {configFile}");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string Get(string key)
        {
            var value = _configuration[$"AppSettings:{key}"];
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Configuration key '{key}' is missing or empty.");
            }
            return value;
        }

        public static int GetTimeOut()
        {
            string timeoutValue = _configuration["Timeout"];
            return int.TryParse(timeoutValue, out int timeout) ? timeout : 30; // Default to 30 seconds if not set    
        }
        public static string GetScreenshotMode()
        {
            return Get("ScreenshotMode") ?? "OnFailure";
        }

    }
}
