using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutomationExcercise.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace AutomationExcercise.StepDefinations
{
    [Binding]
    public class ProductSteps
    {
        private IWebDriver _driver;
        private readonly ProductPage _productPage;

        public ProductSteps(IWebDriver driver, ProductPage productPage)
        {
            _driver = driver;
            _productPage = productPage;
        }
        [StepDefinition("user on the products page")]
        public void GivenUserOnTheProductsPage()
        {
            _productPage.GoToProductPage();
        }

        [StepDefinition("user search for {string}")]
        public void WhenUserSearchFor(string name)
        {
            _productPage.SearchProducts(name);
        }

        [StepDefinition("user should see product {string} in the results")]
        public void ThenUserShouldSeeProductInTheResults(string name)
        {
            if (!_productPage.ProductDisplayed(name))
            {
                throw new Exception($"Product '{name}' was not found in the search results.");
            }
        }

        [StepDefinition("user click on the product {string}")]
        public void WhenUserClickOnTheProduct(string name)
        {
            _productPage.ClickOnProduct(name);
        }

        [StepDefinition("user should be on the product detail page for {string}")]
        public void ThenUserShouldBeOnTheProductDetailPageFor(string name)
        {
            if (!_productPage.VerifyingProductDetails(name))
            {
                throw new Exception($"Product '{name}' was not displayed in product details page.");
            }
        }

        [StepDefinition("the product price and description should be displayed")]
        public void ThenTheProductPriceAndDescriptionShouldBeDisplayed()
        {
            
        }

    }
}
