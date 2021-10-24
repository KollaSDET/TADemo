using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace TADemo
{

    [Binding]
    public class Hooks
    { 
            private readonly IObjectContainer _objectContainer;
        
             public Hooks(IObjectContainer objectContainer)
            {
                _objectContainer = objectContainer;
            }

              
        [BeforeScenario]
            public void BeforeScenario()
            {
            string chromeDriverPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "..\\ChromeBrowser");

            //string chromeDriverPath = Path.Combine(projectDir, );
                IWebDriver driver = new ChromeDriver(chromeDriverPath);
                _objectContainer.RegisterInstanceAs(driver);
            }

            [AfterScenario]
            public void AfterScenario()
            {
            IWebDriver driver = _objectContainer.Resolve<IWebDriver>();
                driver.Quit();
            }
        }
    
}

