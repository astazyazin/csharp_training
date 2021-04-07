using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }
        public List<ContactData> GetContactsList()
        {
            List<ContactData> list = new List<ContactData>();

            return list;
        }

        public void AddContacts(ContactData newContact)
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d58");
            aux.WinWait(CONTACTTITLE);
            aux.ControlSend(CONTACTTITLE,"", "WindowsForms10.EDIT.app.0.2c908d516",newContact.FirstName);
            aux.ControlSend(CONTACTTITLE, "", "WindowsForms10.EDIT.app.0.2c908d515", newContact.MiddleName);
            aux.ControlSend(CONTACTTITLE, "", "WindowsForms10.EDIT.app.0.2c908d513", newContact.LastName);
            aux.ControlClick(CONTACTTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d58");
            aux.WinWait(WINTITLE);

        }
    }
}
