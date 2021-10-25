using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TADemo
{
   
        public static class WebDriverExtensions

        {

        public static IWebElement Find(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            IWebElement element = null;
            wait.Until(d =>
            {
                try
                {
                    element = d.FindElement(by);
                    if (element.Displayed &&
                        element.Enabled)
                    {
                        return element;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                    wait.PollingInterval = TimeSpan.FromMilliseconds(1000);
                    wait.Message = @"Element " + by + " to be searchDefaultWaited not found";
                    element = driver.FindElement(by);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                    return element;

                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Element not found: " + by.ToString());
                    if (d.FindElements(By.XPath("//div[contains(text(),'AADSTS50196')]")).Count > 0)
                        d.Close();

                }
                return null;

            });
            return element;
        }


        public static IReadOnlyCollection<IWebElement> FindMultiple(this IWebDriver driver, By by)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            IReadOnlyCollection<IWebElement> element = null;
            try
            {
                wait.Until(d =>
                {
                    try
                    {
                        element = d.FindElements(by);

                        if
                        (
                        element.Count > 0 &&
                        element.ElementAt(0).Displayed &&
                        element.ElementAt(0).Enabled
                        )

                        {
                            return element;
                        }
                    }

                    catch (NoSuchElementException)
                    {

                    }
                    return null;

                });
            }
            catch (WebDriverTimeoutException)
            {

            }

            return element;
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

