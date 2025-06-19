using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using AutomationExcercise.Utilities;

namespace AutomationExcercise.Drivers
{
    public static class DriverFactory
    {
        private static AsyncLocal<IWebDriver> _driver = new();

        public static IWebDriver GetDriver()
        {
            if (_driver.Value == null)
            {
                string browser = ConfigReader.Get("browser");
                if (string.IsNullOrEmpty(browser))
                {
                    throw new ArgumentException("The 'browser' configuration is missing or invalid.");
                }
                browser = browser.ToLower();
                switch (browser)
                {
                    case "chrome":
                        _driver.Value = new ChromeDriver();
                        break;
                    case "firefox":
                        _driver.Value = new FirefoxDriver();
                        break;
                    case "edge":
                        _driver.Value = new EdgeDriver();
                        break;
                    default:
                        throw new ArgumentException("Unsupported browser in config file");
                }

                _driver.Value.Manage().Window.Maximize();
            }
            return _driver.Value;
        }

        public static void QuitDriver()
        {
            _driver.Value.Quit();
            _driver.Value = null;
        }
    }
}
