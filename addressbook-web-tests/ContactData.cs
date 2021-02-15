using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class ContactData
    {
        private string firstname;
        private string middlename;
        private string lastname;
        private string nickname = "";
        private string title = "";
        private string company = "";
        private string address = "";
        private string telephoneHome = "";
        private string telephoneMobile = "";
        private string telephoneWork = "";
        private string telephoneFax = "";
        private string email = "";
        private string email2 = "";
        private string email3 = "";
        private string homepage = "";
        private int bday = 0;
        private string bmonth = "";
        private string byear = "";
        private int aday = 0;
        private string amonth = "";
        private string ayear = "";
        private string secondsryAddress = "";
        private string secondaryTelephoneHome = "";
        private string note = "";



        public ContactData (string firstname, string middlename, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
        }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string Middlename
        {
            get { return middlename; }
            set { middlename = value; }
        }
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

    }
}
