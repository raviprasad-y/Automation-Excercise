using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationExcercise.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Script;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationExcercise.Pages
{
    public class BasePage
    {
        private readonly IWebDriver _driver;
        private readonly Actions actions;
        private readonly WebDriverWait wait;
        private readonly string mainWindow;
        public BasePage(IWebDriver driver) {
            _driver = driver;
            int time = ConfigReader.GetTimeOut();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(time));
            actions = new Actions(_driver);
            mainWindow = _driver.CurrentWindowHandle;
        }

        protected IWebElement GetElement(By locator) => _driver.FindElement(locator);
        protected IList<IWebElement> GetElements(By locator) => _driver.FindElements(locator);

        //----------waits----------
        protected IWebElement WaitUntilVisible(By locator)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }
        protected IWebElement WaitUntilClickable(By locator)
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }
        protected bool waitUntilInvisible(By locator)
        {
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
        protected bool WaitUntilTextPresent(By locator, string text)
        {
            return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        //----------Element Interactions----------
        protected void Click(By locator)
        {
            WaitUntilClickable(locator).Click();
            UnifiedLogger.Info($"Clicked on element: {locator}");
        }
        protected void SendKeys(By locator, string text)
        {
            var element = WaitUntilClickable(locator);
            element.Clear();
            element.SendKeys(text);
            UnifiedLogger.Info($"Sent Keys '{text}' to element: {locator}");
        }
        protected string GetText(By locator)
        {
            var text = WaitUntilVisible(locator).Text;
            UnifiedLogger.Info($"Text from '{locator}': '{text}'");
            return text;
        }
        protected bool IsDisplayed(By locator)
        {
            try
            {
                return WaitUntilVisible(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        protected void Clear(By locator)
        {
            WaitUntilVisible(locator).Clear();
        }

        //----------DropDowns----------
        protected void SelectByValue(By locator, string value)
        {
            var dropdown = new SelectElement(WaitUntilVisible(locator));
            dropdown.SelectByValue(value);
            UnifiedLogger.Info($"Selected by value '{value}' from dropdown: {locator}");
        }
        protected void SelectByText(By locator, string visibleText)
        {
            var dropdown = new SelectElement(WaitUntilVisible(locator));
            dropdown.SelectByText(visibleText);
            UnifiedLogger.Info($"Selected by Text '{visibleText}' from dropdown: {locator}");
        }
        protected void SelectByIndex(By locator, int index)
        {
            var dropdown = new SelectElement(WaitUntilVisible(locator));
            dropdown.SelectByIndex(index);
            UnifiedLogger.Info($"Selected by Index '{index}' from dropdown: {locator}");
        }

        //----------Alert----------
        private IAlert WaitForAlert()
        {
            return wait.Until(ExpectedConditions.AlertIsPresent());
        }
        protected void AcceptAlert()
        {
            IAlert alert = WaitForAlert();
            alert.Accept();
            UnifiedLogger.Info("Alert Accepted.");
        }
        protected void DismissAlert()
        {
            IAlert alert = WaitForAlert();
            alert.Dismiss();
            UnifiedLogger.Info("Alert Dismissed.");
        }
        protected string GetAlertText()
        {
            IAlert alert = WaitForAlert();
            return alert.Text;
        }
        protected void SendAlertText(string text)
        {
            IAlert alert = WaitForAlert();
            alert.SendKeys(text);
        }

        //----------Window Handles----------
        protected void SwitchtoWindow(int index)
        {
            var handles = _driver.WindowHandles;
            if(index < handles.Count)
            {
                _driver.SwitchTo().Window(handles[index]);
                UnifiedLogger.Info($"Switched to window at index: {index} - handle: {handles[index]}");
            }
            else
            {
                UnifiedLogger.Warn($"Index: {index} is out of bounds for available window handles");
            }
        }
        protected void SwitchToMainWindow()
        {
            _driver.SwitchTo().Window(mainWindow);
            UnifiedLogger.Info("Switched back to main window.");
        }

        //----------iFrames----------
        protected void SwitchToFrame(By locator)
        {
            _driver.SwitchTo().Frame(WaitUntilVisible(locator));
            UnifiedLogger.Info($"Switched to frame: {locator}");
        }
        protected void SwitchToDefaultContent()
        {
            _driver.SwitchTo().DefaultContent();
            UnifiedLogger.Info("Switched to default content from iframe,");
        }

        //----------Actions----------
        protected void Hover(By locator)
        {
            actions.MoveToElement(WaitUntilVisible(locator)).Build().Perform();
            UnifiedLogger.Info($"Hovered over element: {locator}.");
        }
        protected void DoubleClick(By locator)
        {
            actions.DoubleClick(WaitUntilVisible(locator)).Build().Perform();
            UnifiedLogger.Info($"Double clicked on element: {locator}.");
        }
        protected void RightClick(By locator)
        {
            actions.ContextClick(WaitUntilVisible(locator)).Build().Perform();
            UnifiedLogger.Info($"Right clicked on element: {locator}.");
        }
        protected void DragAndDrop(By source, By target)
        {
            actions.DragAndDrop(WaitUntilVisible(source), WaitUntilVisible(target)).Build().Perform();
            UnifiedLogger.Info($"Dragged and dropped from {source} to {target}.");
        }
        protected void EnterUsingActions(By locator, string text)
        {
            actions.MoveToElement(WaitUntilVisible(locator))
                   .Click()
                   .SendKeys(text)
                   .Build()
                   .Perform();
            UnifiedLogger.Info($"Entered text '{text}' using Actions on element: {locator}.");
        }

        //----------scroll----------
        protected void ScrollIntoView(By locator)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            var element = WaitUntilVisible(locator);
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            UnifiedLogger.Info($"Scrolled into view: {locator}");
        }
        protected void ScrollToBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.ScrollTo(0, document.body.scrollHeight);");
            UnifiedLogger.Info("Scrolled to the bottom of the page.");
        }


    }
}
