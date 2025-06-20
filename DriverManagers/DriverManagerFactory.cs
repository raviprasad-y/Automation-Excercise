using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using AutomationExcercise.Utilities;
using AutomationExercise.DriverManagers;

namespace AutomationExcercise.Drivers
{
    public static class DriverManagerFactory
    {
        public static Type GetDriverManagerType()
        {
            string browser = ConfigReader.Get("browser");
            return browser.ToLower() switch
            {
                "chrome" => typeof(ChromeDriverManager),
                "firefox" => typeof(FirefoxDriverManager),
                "Edge" => typeof(EdgeDriverManager),
                _ => throw new NotSupportedException($"Browser {browser} is not supported")
            };
        }
    }
}
