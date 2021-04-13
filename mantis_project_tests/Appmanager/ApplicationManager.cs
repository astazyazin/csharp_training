using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_project_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public LoginHelper Auth { get; private set; }
        public ManagementMenuHelper Menu { get; private set; }
        public ProjectManagementHelper Project { get; private set; }


        //    private StringBuilder verificationErrors;


        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager ()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            baseURL = "http://localhost";

            Auth = new LoginHelper(this);
            Menu = new ManagementMenuHelper(this);
            Project = new ProjectManagementHelper(this);


        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        public static ApplicationManager GetInstance ()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.25.0/login_page.php";
                app.Value= newInstance;
                
            }
            return app.Value;
        }
        public IWebDriver Driver
        { 
            get
            {
                return driver;
            }
        }
        
        

        
    }
}
