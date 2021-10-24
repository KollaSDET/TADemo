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
  
        public IWebElement ProductStore_label => WebDriver.Find(By.Id("nava"), 20);
      
        public IWebElement ProductListContainer => WebDriver.Find(By.Id("contcont"), 10);
        public IWebElement AddToCart_button(string btnName) => WebDriver.Find(By.XPath("//div[@id='tbodyid']//a[contains(@class,'btn btn-success') and text() ='"+btnName+"']"),10);
      
        public IList<IWebElement> pDetailsContainer => WebDriver.FindELEMS(By.XPath("//div[@id='tbodyid']//*[@class='card-title']"), 60);
 


        public IWebElement prdName => WebDriver.Find(By.XPath("//div[@id='tbodyid']//*[@class='name']"), 20);
        public IWebElement prdPrice => WebDriver.Find(By.XPath("//div[@id='tbodyid']//*[@class='price-container']"),20);

        public void NavigateToHomePage(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClickMenuItem(string item)
        {
            var menuItem_Xpath = "//div[@id='contcont']//div[@class='list-group']//a[@class='list-group-item' and contains(text(),item)]";
            //IList<IWebElement> pList = ProductListContainer.FindElements(By.Id("itemc"));
            //var mItem = pList.FirstOrDefault(x => x.Text == item);
            WebDriver.WaitForPageToLoad(By.XPath(menuItem_Xpath),40);
            var mItem = WebDriver.FindElement(By.XPath("//div[@id='contcont']//div[@class='list-group']//a[contains(@onclick,'notebook') and contains(text(),item)]")); 
           mItem.Click();
        }


        public string GetStoreLabelValue()
        {
            var label_Xpath = @"//*[@Id='nava']";
            WebDriver.WaitForPageToLoad(By.XPath(label_Xpath), 40);
            return ProductStore_label.Text;
        }
        public string FindProductItemAndClick(string pItem)
        {
            IWebElement card =null;
            string actualProdCost = "";
            //IList<IWebElement> pcardTitles = pDetailsContainer;//.FindElements(By.ClassName("card-title"));//"//div[@id='tbodyid']//FindElement(By.Id("tbodyid")). //div[@id='contcont']
         
            var target_xpath = "//div[@id='contcont']//div[@class='row'and @id='tbodyid']//h4[@class='card-title']//a[contains(text(),pItem)]";// 'Sony vaio i7')]";

             WebDriver.WaitForPageToLoad(By.XPath(target_xpath), 40);

            IWebElement tile = WebDriver.Find(By.XPath(target_xpath),30);
           
            if (!string.IsNullOrEmpty(tile.Text ))
            {
                var tiles = WebDriver.FindElements(By.XPath(target_xpath));
                
                Assert.AreEqual(pItem, tiles.FirstOrDefault(x => x.Text.Contains(pItem)).Text);
                actualProdCost = tiles.FirstOrDefault(x => x.Text.Contains(pItem)).FindElement(By.XPath("//parent::h4/following-sibling::h5")).Text;
                tiles.FirstOrDefault(x => x.Text.Contains(pItem)).Click();
              
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
            if (WebDriver.SwitchTo().Alert().Text == expText)
                WebDriver.SwitchTo().Alert().Accept();

        }



    }
}
