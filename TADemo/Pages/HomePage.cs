using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using static OpenQA.Selenium.Support.Extensions.WebDriverExtensions;

namespace TADemo.Pages
{
    class HomePage
    {
        private readonly IWebDriver WebDriver;

        public HomePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        public IList<IWebElement> arrayChallengeTableRows => WebDriver.FindElements(By.XPath("//section[@id='challenge']//table/tbody/tr/td[contains(@data-test-id,'array-item')]/parent::tr"));

        public IWebElement inputChallengeResult2 => WebDriver.Find(By.XPath("//section[@id='challenge']//input[@id='undefined-submitchallenge1-undefined-16475' and @data-test-id = 'submit-2']"), 10);
        public IWebElement ProductStore_label => WebDriver.Find(By.Id("nava"),10);
        //public IWebElement ProductList => WebDriver.FindElement(By.XPath(//a[@id='cat']/));
            public IWebElement ProductList => WebDriver.Find(By.Id("contcont"),10);
        public void NavigateToHomePage(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClickProductItem(string item)
        {
            IList<IWebElement> pList = ProductList.FindElements(By.Id("itemc"));
            var mItem = pList.FirstOrDefault(x => x.Text == item);
            mItem.Click();
        }


    }
}
