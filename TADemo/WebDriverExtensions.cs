using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TADemo
{
   
        public static class WebDriverExtensions

        {


        public static IWebElement Find(this IWebDriver driver, By by, int timeoutInSeconds)

        {

            if (timeoutInSeconds > 0)

            {

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                wait.PollingInterval = TimeSpan.FromMilliseconds(1000);
                wait.Message = @"Element " + by + " to be searchDefaultWaited not found";
                return (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by)));
               // return wait.Until(drv => drv.FindElement(by));

            }

            return driver.FindElement(by);

        }


        public static IWebElement WaitForPageToLoad(this IWebDriver driver, By elementLocator, int timeoutInSeconds)
        {
            int tries = 0;
            do
            {
                try
                {
                    tries++;


                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                    wait.PollingInterval = TimeSpan.FromMilliseconds(1000);

                    wait.Message = @"Element " + elementLocator + " to be searchDefaultWaited not found";

                    return (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator)));
                    //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                }
                // catch (ElementNotVisibleException)
                catch (StaleElementReferenceException)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                    wait.PollingInterval = TimeSpan.FromMilliseconds(1000);
                    wait.Message = @"Element " + elementLocator + " to be searchDefaultWaited not found";
                    IWebElement elt = driver.FindElement(elementLocator);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(elt));
                    return elt;

                }
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));}
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                    throw;
                }



            } while (tries < 3);
        }
      

        public static IList<IWebElement> FindELEMS(this IWebDriver driver, By by, int timeoutInSeconds)

        {
            IList<IWebElement> elementList = new List<IWebElement>();

            if (timeoutInSeconds > 0)

            {

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                return wait.Until(drv => drv.FindElements(by));

            }

            return driver.FindElements(by);

        }

    }
   }

