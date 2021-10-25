using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
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
  
        
        public By label_Xpath => By.XPath("//nav[@id='narvbarx']//a[@id='nava']");
        public IWebElement ProductStore_label => WebDriver.Find(label_Xpath, 30);

        public IWebElement ProductListContainer => WebDriver.Find(By.Id("contcont"), 10);
        public IWebElement AddToCart_button(string btnName) => WebDriver.Find(By.XPath("//div[@id='tbodyid']//a[contains(@class,'btn btn-success') and text() ='"+btnName+"']"),20);
      
        public IList<IWebElement> pDetailsContainer => WebDriver.FindELEMS(By.XPath("//div[@id='tbodyid']//*[@class='card-title']"), 60);
 


        public IWebElement prdName => WebDriver.Find(By.XPath("//div[@id='tbodyid']//*[@class='name']"), 40);
        public IWebElement prdPrice => WebDriver.Find(By.XPath("//div[@id='tbodyid']//*[@class='price-container']"),40);

        public void NavigateToHomePage(string url)
        {
            //WebDriver.Navigate().GoToUrl(url);
            WebDriver.Navigate().GoToUrl(url);
            IWebElement elt = null;
            bool bFlag = false;
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
            int tries = 0;
            while ((!bFlag) || tries < 2)
            {
                try
                {
                    tries++;

                    elt = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("nava")));
                    
                    Console.WriteLine("while" + bFlag + "  " + tries);
                    bFlag = true;
                    //return bFlag;

                }
                catch (StaleElementReferenceException)
                {
                    bFlag = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(WebDriver.FindElement(By.Id("nava"))));
                    //Console.WriteLine(bFlag + "  " + tries);
                    
                }
                    
            }
           // return bFlag;
        }
       
        public void ClickMenuItem(string item)
        {
            //var menuItem_Xpath = "//div[@id='contcont']//div[@class='list-group']//a[@class='list-group-item' and contains(text(),item)]";
            //IList<IWebElement> pList = ProductListContainer.FindElements(By.Id("itemc"));
            //var mItem = pList.FirstOrDefault(x => x.Text == item);
           
            var menuItem_Xpath = "//div[@id='contcont']//div[@class='list-group']//a[contains(@onclick,'notebook') and contains(text(),item)]";
            WebDriver.WaitForPageToLoad(By.XPath(menuItem_Xpath),40);
          
            var mItem = WebDriver.Find(By.XPath(menuItem_Xpath),10);
            mItem.Click();
        }


        public string GetStoreLabelValue()
        {            
           return ProductStore_label.Text;
        }
        public string FindProductItemAndClick(string pItem)
        {
            IWebElement card =null;
            string actualProdCost = "";
            var target_xpath = "//div[@id='contcont']//h4[@class='card-title']//a[contains(text(),'"+pItem+"')]";
            //WebDriver.WaitForPageToLoad(By.XPath(target_xpath), 40);
            //var tilesList = WebDriver.FindMultiple(By.XPath(target_xpath));
            var textContainer = ProductListContainer.Text;
            IWebElement tile = WebDriver.Find(By.XPath(target_xpath), 30);
           var tiles = WebDriver.FindELEMS(By.XPath(target_xpath),40);
            // tile = tiles.FirstOrDefault(x => x.Text.Contains(pItem));
            //var tiles = WebDriver.FindElements(By.XPath(target_xpath));
            Assert.AreEqual(pItem, WebDriver.FindMultiple(By.XPath(target_xpath)).FirstOrDefault(x => x.Text.Contains(pItem)).Text);
            if (!string.IsNullOrEmpty(WebDriver.FindMultiple(By.XPath(target_xpath)).FirstOrDefault(x => x.Text.Contains(pItem)).Text ))
            {
                actualProdCost = tiles.FirstOrDefault(x => x.Text.Contains(pItem)).FindElement(By.XPath("//parent::div[@class='card-block']//child::h5")).Text;
                tile.Click();
              
            }
            else
            {
                //Assert.IsTrue("B"=="C");
                actualProdCost = "";
            }
              
            
            return actualProdCost;// actualProdCost;  

        }

        public List<string> GetProductItemDetails(string p)
        {
         
            IList<string> pDetailList = new List<string>();
          
           var name=  prdName.Text;
            var price = prdPrice.Text; 
            pDetailList.Add(name);
            pDetailList.Add(price);
            return ((List<string>)pDetailList);
        }

        public void ClickAddToCartButton(string btnName)
        {
            AddToCart_button(btnName).Click();
        }

        public void ClickBrowserAlert(string expText)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());


            if (WebDriver.SwitchTo().Alert().Text.Contains(expText))
                WebDriver.SwitchTo().Alert().Accept();

        }



    }
}
