using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {

            if (GroupData.GetAll().Count == 0)  
            {
                if (ContactData.GetAll().Count == 0) // если нет ни контактов ни групп, создаем все и добавляем контакт в группу 
                {
                    ContactData lostcontact = new ContactData("firstname", "middlename", "lastname");
                    app.Contacts.Create(lostcontact);

                    GroupData lostgroup = new GroupData("newgroup", "header", "footer");
                    app.Groups.Create(lostgroup);
                                       
                }
                else //если нет групп, но есть контакты , то создаем группу и добавляем контакт в группу
                {
                    GroupData lostgroup = new GroupData("newgroup", "header", "footer");
                    app.Groups.Create(lostgroup);
                }

                GroupData newgroup = GroupData.GetAll()[0];
                List<ContactData> old = newgroup.GetContacts();
                ContactData newcontact = ContactData.GetAll().Except(old).First();
                app.Contacts.AddContactToGroup(newcontact, newgroup);

            }
            if (ContactData.GetAll().Count == 0) //если нет  контактов , то создаем контакт и добавляем контакт в группу
            {
                ContactData lostcontact = new ContactData("firstname", "middlename", "lastname");
                app.Contacts.Create(lostcontact);

                GroupData newgroup = GroupData.GetAll()[0];
                List<ContactData> old = newgroup.GetContacts();
                ContactData newcontact = ContactData.GetAll().Except(old).First();
                app.Contacts.AddContactToGroup(newcontact, newgroup);
            }
                       
            int num = 0;
            for (int i = 0; i < GroupData.GetAll().Count; i ++ ) // ищем группу где есть хотя бы один контакт
            {
                GroupData somegroup = GroupData.GetAll()[i];
                if (somegroup.GetContacts().Count != 0)
                {
                    num = i;
                }
            }
            if (num == 0) //если все группы пустые - берем первую и добавляем туда контакт
            {
                GroupData newgroup = GroupData.GetAll()[0];
                List<ContactData> old = newgroup.GetContacts();
                ContactData newcontact = ContactData.GetAll().Except(old).First();
                app.Contacts.AddContactToGroup(newcontact, newgroup);
            }

            GroupData group = GroupData.GetAll()[num];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList.First();

            app.Contacts.RemoveContactFromGroup(group,contact);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
