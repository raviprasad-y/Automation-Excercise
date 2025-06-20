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
    public class ProductPage : BasePage
    {
        private readonly IWebDriver _driver;

        public ProductPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        //Locators
        private readonly By ProductsLink = By.XPath("//a[@href='/products']");
        private readonly By ProductsSearch = By.XPath("//input[@id='search_product']");
        private readonly By SearchButton = By.XPath("//button[@id=\'submit_search\']");
        private readonly By SearchResults = By.XPath("//div[@class='features_items']");
        private static By Result(string name) => By.XPath($"//div[@class='productinfo text-center']//p[contains(text(),'{name}')]");
        private static By ViewProductButton(string productName) =>
            By.XPath($"//p[contains(text(),'{productName}')]/parent::div/parent::div/following-sibling::div//a[normalize-space()='View Product']");
        private static By Price(string name) => By.XPath($"//h2[text()='{name}']/parent::div//span[normalize-space()='Rs. 400']");
        public void GoToProductPage()
        {
            Click(ProductsLink);
        }
        public void SearchProducts(string name)
        {
            SendKeys(ProductsSearch, name);
            Click(SearchButton);
        }
        public IList<string> SearchResultsList()
        {
            if (GetElements(SearchResults).Count == 0)
            {
                throw new NoSuchElementException("No products found for the given search criteria.");
            }
            return GetElements(SearchResults).Select(result => result.Text).ToList();
        }
        public bool ProductDisplayed(string name)
        {
            ScrollIntoView(Result(name));
            return IsDisplayed(Result(name));
        }
        public void ClickOnProduct(string productName)
        {
            ScrollIntoView(Result(productName));
            Click(ViewProductButton(productName));  
        }
        public bool VerifyingProductDetails(string name)
        {

            return IsDisplayed(Price(name));
        }
    }
}

