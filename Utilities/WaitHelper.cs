using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationExcercise.Utilities
{
    public class WaitHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public WaitHelper(IWebDriver driver)
        {
            _driver = driver;
            int timeout = ConfigReader.GetTimeOut();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
        }
        public IWebElement WaitForElementToBeVisible(IWebElement element)
        {
            _wait.Until(driver =>
            {
                try
                {
                    return element.Displayed && element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            return element;
        }
        public IWebElement WaitForElementToBeClickable(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }
        public bool WaitForUrlContains(String partialUrl)
        {
            return _wait.Until(ExpectedConditions.UrlContains(partialUrl));
        }
    }
}
