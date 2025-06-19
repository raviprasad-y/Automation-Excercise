using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutomationExcercise.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutomationExcercise.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelper _wait;
        private readonly Actions actions;

        public ProductPage(IWebDriver driver, WaitHelper wait)
        {
            _driver = driver;
            _wait = wait;
            actions = new Actions(_driver);
        }

        //Locators
        private IWebElement ProductsLink => _driver.FindElement(By.XPath("//a[@href='/products']"));
        private IWebElement ProductsSearch => _driver.FindElement(By.XPath("//input[@id='search_product']"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//button[@id=\'submit_search\']"));
        private IList<IWebElement> SearchResults => _driver.FindElements(By.XPath("//div[@class='features_items']"));
        private IWebElement Result(string name) => _driver.FindElement(By.XPath("//div[@class='overlay-content']//p[contains(text(),'"+ name +"')]"));
        private IWebElement ViewProduct => _driver.FindElement(By.XPath("//a[normalize-space()='View Product']"));
        private IWebElement ViewProductButton(string productName) =>
            _driver.FindElement(By.XPath($"//p[contains(text(),'{productName}')]/parent::div/parent::div/following-sibling::div//a[normalize-space()='View Product']"));
        private IWebElement Price(string name) => _driver.FindElement(By.XPath($"//h2[text()='{name}']/parent::div//span[normalize-space()='Rs. 400']"));
        public void GoToProductPage()
        {
            _wait.WaitForElementToBeVisible(ProductsLink).Click();
            _wait.WaitForElementToBeVisible(ProductsSearch);
        }
        public void SearchProducts(string name)
        {
            _wait.WaitForElementToBeVisible(ProductsSearch).SendKeys(name);
            
            SearchButton.Click();
            //ProductsSearch.Submit();
        }
        public IList<string> SearchResultsList()
        {
            if (SearchResults.Count == 0)
            {
                throw new NoSuchElementException("No products found for the given search criteria.");
            }
            return SearchResults.Select(result => result.Text).ToList();
        }
        public bool ProductDisplayed(string name)
        {
            return _wait.WaitForElementToBeVisible(Result(name)).Displayed;
            
        }
        public void ClickOnProduct(string productName)
        {
            ViewProductButton(productName).Click();
            
        }
        public bool VerifyingProductDetails(string name)
        {

            return _wait.WaitForElementToBeVisible(Price(name)).Displayed;
        }
    }
}

