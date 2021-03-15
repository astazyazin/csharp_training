using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30), GenerateRandomString(30))
                {
                    Address = GenerateRandomString(50),
                    HomePhone = GeneratePhoneNumber(),
                    MobilePhone = GeneratePhoneNumber(),
                    WorkPhone = GeneratePhoneNumber(),
                    Email = GenerateRandomString(10)+ "@"+ GenerateRandomString(10) + "." + GenerateRandomString(4),
                    Email2 = GenerateRandomString(10) + "@" + GenerateRandomString(10) + "." + GenerateRandomString(4),
                    Email3 = GenerateRandomString(10) + "@" + GenerateRandomString(10) + "." + GenerateRandomString(4)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            
            List<ContactData> oldContacts = app.Contacts.GetContactList(); 

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetGroupCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        

        
    }
}
