using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;



namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            //если нет контакта- создаем
            
            if (!app.Contacts.IsContactsPresent())
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contacts.Create(newcontact);
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];
            app.Contacts.RemovalById(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetGroupCount());

            oldContacts.RemoveAt(0);
            List<ContactData> newContacts = ContactData.GetAll();
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }

        }
    }
}
