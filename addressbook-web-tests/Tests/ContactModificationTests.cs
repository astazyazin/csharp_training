using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData("firstname_mod", "middlename_mod", "lastname_mod");
            //если нет контакта- создаем
            bool isContactPresent = app.Contact.IsElementPresent(By.Name("selected[]"));

            if (!isContactPresent)
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contact.Create(newcontact);
            }
            app.Contact.Modify(contact);
        }
    }
}
