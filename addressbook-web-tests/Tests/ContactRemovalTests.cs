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
            bool isContactPresent = app.Contacts.IsElementPresent(By.Name("selected[]"));

            if (!isContactPresent)
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contacts.Create(newcontact);
            }
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[0];
            app.Contacts.Removal(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetGroupCount());

            oldContacts.RemoveAt(0);
            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }

        }
    }
}
