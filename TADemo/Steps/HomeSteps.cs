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
            Assert.AreEqual(p0,_homePage.ProductStore_label.Text);
        }
        [Given(@"I have clicked on ""(.*)"" menu item")]
        public void GivenIHaveClickedOnMenuItem(string itemName)
        {
            _homePage.ClickProductItem(itemName);
        }

        [Then(@"I have clicked on ""(.*)"" product item")]
        public void ThenIHaveClickedOnProductItem(string p0)
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I have seen below product details")]
        public void ThenIHaveSeenBelowProductDetails(Table table)
        {
            //ScenarioContext.Current.Pending();
        }

    }
}
