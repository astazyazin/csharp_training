using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            //если нет контактов или нет контактов без групп - создаем новый контакт
            if (ContactData.GetAll().Count == 0 || (app.Contacts.FindContactWithoutGroup().GetNumberOfSearchResults(0) == 0))
            {
                ContactData newcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contacts.Create(newcontact);
            }
            // если нет групп - создаем новую
            if (GroupData.GetAll().Count == 0)
            {
                GroupData newgroup = new GroupData("newgroup", "header", "footer");
                app.Groups.Create(newgroup);
            }
            
            
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            //actions
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
