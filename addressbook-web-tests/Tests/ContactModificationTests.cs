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
            bool isContactPresent = app.Contacts.IsElementPresent(By.Name("selected[]"));

            if (!isContactPresent)
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contacts.Create(newcontact);
            }
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldData = oldContacts[0];

            app.Contacts.Modify(0,contact);

            Assert.AreEqual(oldContacts.Count , app.Contacts.GetGroupCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = contact.Firstname;
            oldContacts[0].Lastname = contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            
            foreach (ContactData contacts in newContacts)
            {
                if (contacts.Id == oldData.Id)
                {
                    Assert.AreEqual(contact.Firstname, contacts.Firstname);
                    Assert.AreEqual(contact.Lastname, contacts.Lastname);
                }
            }
        }
    }
}
