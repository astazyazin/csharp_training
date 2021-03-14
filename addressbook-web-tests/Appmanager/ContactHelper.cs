using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {

        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string firstName = cells[2].Text;
            string lastName = cells[1].Text;
            string address = cells[3].Text;

            string allPhone = cells[5].Text;

            string allEmail = cells[4].Text;  
            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhone,
                AllEmails = allEmail
            };
        }

        public ContactData GetContactInformationFromDetailsForm(int index)
        {
            manager.Navigator.OpenHomePage();
            ShowDetailsInfo(index);
            string allDetails = Regex.Replace((driver.FindElement(By.Id("content")).Text),"[ \r\nH:M:W:]","");
            return new ContactData("", "")
            {
                DetailsInfo = allDetails
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModify(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, middleName, lastName)
            {
                Address = address, 
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
            
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Removal(int p)
        {
           
            SelectContact(p);
            DeletingContact();
            return this;
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("tr[name=entry]")).Count;
        }

        public ContactHelper Modify(int p,ContactData contact)
        {
            InitContactModify(p);
            FillContactForm(contact);
            SubmitContactUpdate();
            ReturnToHomePage();
            return this;
        }

        private List<ContactData> contactCashe = null;

        public List<ContactData> GetContactList()
        {
            if (contactCashe== null)
            {
                contactCashe = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
                foreach (IWebElement element in elements)
                {
                    contactCashe.Add(new ContactData(element.FindElement(By.XPath("td[3]")).Text, element.FindElement(By.XPath("td[2]")).Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            
            return new List<ContactData>(contactCashe);
        }

        public bool IsContactsPresent()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click(); 
            return this;
        }
        public ContactHelper SubmitContactUpdate()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactCashe = null;
            return this;
        }

        public ContactHelper InitContactModify(int index)
        {
            driver.FindElement(By.XPath("//tr[@name='entry'][" + (index + 1) + "]//a//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper ShowDetailsInfo(int index)
        {
            driver.FindElement(By.XPath("//tr[@name='entry'][" + (index + 1) + "]//a//img[@alt='Details']")).Click();
            return this;
        }

        public ContactHelper DeletingContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCashe = null;
            driver.SwitchTo().Alert().Accept();
            driver.FindElement(By.LinkText("home")).Click();
            return this; 
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCashe = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
