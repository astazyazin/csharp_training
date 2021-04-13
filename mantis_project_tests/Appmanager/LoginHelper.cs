using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_project_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager)
            : base(manager)
        {

        }
        public void Login(AccountData account) //made
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }
            Type(By.Id("username"), account.Name);
            SubmitLoginInfo();
            Type(By.Id("password"), account.Password);
            SubmitLoginInfo();
            if (IsElementPresent(By.Id("password-current")) 
                & IsElementPresent(By.Id("password")) 
                & IsElementPresent(By.Id("password-confirm")))
            {
                Type(By.Id("password-current"), account.Password);
                Type(By.Id("password"), account.Password);
                Type(By.Id("password-confirm"), account.Password);
                SubmitLoginInfo();
            }
                
        }

        private void SubmitLoginInfo()
        {
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        public bool IsLoggedIn(AccountData account) //made
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Name;
        }

        private string GetLoggedUserName() //made
        {
            return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }

        public bool IsLoggedIn() //made
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
                driver.FindElement(By.Name("user"));
            }

        }
    }
}
