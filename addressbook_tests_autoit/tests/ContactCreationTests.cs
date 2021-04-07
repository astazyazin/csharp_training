using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void TestContactCreation()
        {
       //     List<ContactData> oldContacts = app.Contacts.GetContactsList();

            ContactData newContact = new ContactData()
            {
                FirstName = "fname",
                MiddleName = "mname",
                LastName = "lname"

            };

            app.Contacts.AddContacts(newContact);

    /*        oldContacts.Add(newContact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts,newContacts); */
        }
    }
}
