using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleBrowser.WebDriver;
using OpenQA.Selenium;


namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php";
            IList <IWebElement> elements = driver.FindElements(By.CssSelector("tbody > tr"));
            foreach (IWebElement row in elements)
            {
               IWebElement link =  row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href,@"\d+$");
                string id = m.Value;

                accounts.Add(new AccountData()
                {
                    Name = name, Id = id
                });
            }
            return accounts;
        }
        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver=  OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//input[@type='submit'][@value='Delete User']")).Click();
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver
            {
                Url = baseUrl + "/login_page.php"
            };
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            return driver;
        }

    }
}
