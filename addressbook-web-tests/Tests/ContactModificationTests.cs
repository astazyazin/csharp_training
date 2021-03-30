using System.Collections.Generic;
using NUnit.Framework;



namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData("firstname_mod", "middlename_mod", "lastname_mod");
            //если нет контакта- создаем
            
            if (!app.Contacts.IsContactsPresent())
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contacts.Create(newcontact);
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];

            app.Contacts.ModifyById(oldData, contact);

            Assert.AreEqual(oldContacts.Count , app.Contacts.GetGroupCount());

            List<ContactData> newContacts = ContactData.GetAll();
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
