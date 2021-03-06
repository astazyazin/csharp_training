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
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            //если нет контакта- создаем
            bool isContactPresent = app.Contact.IsElementPresent(By.Name("selected[]"));

            if (!isContactPresent)
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contact.Create(newcontact);
            }
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Removal(0);

          //  List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts.RemoveAt(0);
            List<ContactData> newContacts = app.Contact.GetContactList();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
