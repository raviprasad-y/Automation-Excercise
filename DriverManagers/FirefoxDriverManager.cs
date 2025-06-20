using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace AutomationExercise.DriverManagers
{
    public class FirefoxDriverManager : IDriverManager
    {
        private IWebDriver _driver;

        public IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                var options = new FirefoxOptions();
                options.AddArgument("-start-maximized");
                _driver = new FirefoxDriver(options);
            }
            return _driver;
        }

        public void QuitDriver()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _driver = null;
        }
    }
}
