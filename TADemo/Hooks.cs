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

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
           // LaunchApp();
        }
        public static void LaunchApp()
        {

            foreach (Process proc in Process.GetProcessesByName("node"))
            {
                proc.Kill();
            }

        
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "qa-tech-test-master");
            startInfo.Arguments = "/C npm start";
            process.StartInfo = startInfo;
            
            try
            {
                var pVal = process.Start();
                Thread.Sleep(TimeSpan.FromSeconds(80));
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            
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

