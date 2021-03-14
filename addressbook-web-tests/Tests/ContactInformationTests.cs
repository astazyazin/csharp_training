using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestDetailsInformation()
        {
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(5);
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetailsForm(5);

            //verification
            Assert.AreEqual(fromForm.DetailsInfo, fromDetails.DetailsInfo);
            
        }
    }
}
