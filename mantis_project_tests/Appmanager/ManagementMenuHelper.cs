using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace mantis_project_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager)
            : base(manager)
        {

        }

        public void GoToManageProjectsPage()
        {
            driver.FindElement(By.XPath("//a[contains(@href,'manage_overview')]")).Click();
                
        }

        public void GoToManageProjectsTab()
        {
            driver.FindElement(By.XPath("//a[contains(@href,'manage_proj_page')]")).Click();
        }
    }
}
