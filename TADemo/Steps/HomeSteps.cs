using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TADemo.Pages;
using TechTalk.SpecFlow;

namespace TADemo.Steps
{
    [Binding]
    class HomeSteps
    {
        private readonly HomePage _homePage;
        public  string actualProdCost { get; set; }

        const string homePageUrl = "https://www.demoblaze.com/";
        public HomeSteps(HomePage homePage)
        {
            _homePage = homePage;
        }
        [Given(@"I have navigated to DemoBlaze home page")]
        public void GivenIHaveNavigatedToDemoBlazeHomePage()
        {
            _homePage.NavigateToHomePage(homePageUrl);
        }

        [Given(@"I have seen ""(.*)"" as the main label")]
        public void GivenIHaveSeenAsTheMainLabel(string p0)
        {
            Assert.AreEqual(p0,_homePage.GetStoreLabelValue());
        }
        [Given(@"I have clicked on ""(.*)"" menu item")]
        public void GivenIHaveClickedOnMenuItem(string itemName)
        {
            _homePage.ClickMenuItem(itemName);
        }

        [Then(@"I have clicked on ""(.*)"" product item")]
        public void ThenIHaveClickedOnProductItem(string p0)
        {
          
            actualProdCost = _homePage.FindProductItemAndClick(p0);
        }

      
        [Then(@"I have verified product item ""(.*)"" on product details page")]
        public void ThenIHaveVerifiedProductItemOnProductDetailsPage(string p0)
        {
            IList<string> expectedProdcuctItemDetails = new List<string> { p0, actualProdCost+" *includes tax" };
            IList<string> actualProductItemDetails = _homePage.GetProductItemDetails(p0);
            Assert.AreEqual(expectedProdcuctItemDetails, actualProductItemDetails);
        }
        [Then(@"I have clicked on ""(.*)"" button")]
        public void ThenIHaveClickedOnButton(string btnName)
        {
            _homePage.ClickAddToCartButton(btnName);
        }
        [Then(@"I have accepted an alert as ""(.*)""")]
        public void ThenIHaveAcceptedAnAlertAs(string p0)
        {
            _homePage.ClickBrowserAlert(p0);
        }
               





    }
}
