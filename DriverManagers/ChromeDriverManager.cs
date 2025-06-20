using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationExercise.DriverManagers
{
    public class ChromeDriverManager : IDriverManager
    {
        private IWebDriver _driver;
        public IWebDriver GetDriver()
        {
            if(_driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                _driver = new ChromeDriver(options);
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
