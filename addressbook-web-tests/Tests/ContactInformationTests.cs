using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : ContactTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(1);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(1);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestDetailsInformation()
        {
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(1);
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetailsForm(1);

            //verification
            Assert.AreEqual(fromForm.DetailsInfo, fromDetails.DetailsInfo);
            
        }
    }
}
