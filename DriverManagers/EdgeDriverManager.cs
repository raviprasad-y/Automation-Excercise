using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace AutomationExercise.DriverManagers
{
    public class EdgeDriverManager : IDriverManager
    {
        private IWebDriver _driver;

        public IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                var options = new EdgeOptions();
                options.AddArgument("start-maximized");
                _driver = new EdgeDriver(options);
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
