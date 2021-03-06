using System.Collections.Generic;
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
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].Firstname = contact.Firstname;
            oldContacts[0].Lastname = contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
