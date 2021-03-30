using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table (Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        private string allPhones;
        private string allEmails;
        private string detailsInfo;
        private string bday;
        private string bmonth;
        private string aday;
        private string amonth;

        public ContactData()
        {
            
        }
        public ContactData (string firstname, string middlename, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
            Middlename = middlename;
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
            Middlename = null;
           
        }
        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "middlename")]
        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homepage")]
        public string Homepage { get; set; }

        [Column(Name = "bday")]
        public string Bday 
        { get {
                if (String.Compare(bday, "0") == 0)
                {
                    return "";
                }
                return bday; } 
          set {
                bday = value; }
        }

        [Column(Name = "bmonth")]
        public string Bmonth
        {
            get
            {
                if (String.Compare(bmonth, "-") == 0)
                {
                    return "";
                }
                return bmonth;
            }
            set
            {
                bmonth = value;
            }
        }

        [Column(Name = "byear")]
        public string Byear { get; set; }

        [Column(Name = "aday")]
        public string Aday
        {
            get
            {
                if (String.Compare(aday, "0") == 0)
                {
                    return "";
                }
                return aday;
            }
            set
            {
                aday = value;
            }
        }

        [Column(Name = "amonth")]
        public string Amonth
        {
            get
            {
                if (String.Compare(amonth, "-") == 0)
                {
                    return "";
                }
                return amonth;
            }
            set
            {
                amonth = value;
            }
        }

        [Column(Name = "ayear")]
        public string Ayear { get; set; }
        
        [Column(Name = "address2")]
        public string SecAdress { get; set; }

        [Column(Name = "phone2")]
        public string SecHomePhone { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

        [Column(Name = "id"),PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        { 
            get
            { 
                if (allPhones!=null)
                {
                    return allPhones;
                }
                else
                {
                    
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            } 
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (Email + "\r\n" + Email2 + "\r\n" + Email3).Trim();
                    
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string DetailsInfo
        {
            get
            {
                if (detailsInfo != null)
                {
                    return detailsInfo;
                }
                else
                {

                    return ((CleanUpName(Firstname,"") + CleanUpName(Middlename,"") + CleanUpName(Lastname,"")).Trim() + "\r\n"
                                + CleanUpPlus(Nickname)
                                + CleanUpPlus(Title)
                                + CleanUpPlus(Company)

                                + CleanUpPlus(Address) + "\r\n"
                                + CleanUpPhone(HomePhone, "H: ")
                                + CleanUpPhone(MobilePhone, "M: ")
                                + CleanUpPhone(WorkPhone, "W: ")
                                + CleanUpPhone(Fax, "F: ") + "\r\n"
                                + CleanUpPlus(Email)
                                + CleanUpPlus(Email2)
                                + CleanUpPlus(Email3)
                                + CleanUpPhone((Homepage).Trim(), ("Homepage:" + "\r\n") ) + "\r\n"
                                + CleanUpDates(Bday,Bmonth,Byear, "Birthday ")
                                + CleanUpDates(Aday, ChangeFirstLetterToUppercase(Amonth), Ayear, "Anniversary ") + "\r\n"
                                + CleanUpPlus(SecAdress) + "\r\n"
                                + CleanUpPhone(SecHomePhone, "P: ") + "\r\n"
                                + CleanUpPlus(Notes)
                                )
                                .Trim();
                }
            }
            set
            {
                detailsInfo = value;
            }
        }
        private string ChangeFirstLetterToUppercase (string text)
        {
            if (text == null || text == "")
            {
                return "";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder(text);
            sb[0] = System.Char.ToUpper(sb[0]);
            return sb.ToString();
        }
        private string CleanUpName(string detail, string postfix)
        {
            if (detail == null || detail == "")
            {
                return "";
            }
            return (detail + postfix + " ");
        }
        private string CleanUpPhone(string phone, string prefix)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return (prefix +  phone + "\r\n");
        }

        private string Age (string year, string month, string day)
        {
            if ((day == null || day == "") && (month == null || month == "") && (year == null || year == ""))
            {
                return "";//если ничего не выбрано ,возвращаем пустую строку
            }
            if (day == "" || day == "0")
            {
                day = "1"; //ессли не выбран день, то по  умолчанию - первой число
            }
            if (month == "" || month == "-")
            {
                month = "1";//если не выбран месяц, то по  умолчанию - первый месяц
            }
            string yearnow = DateTime.Now.Year.ToString(); // получаем текущий год
            string input = yearnow + "/" + month + "/" + day; // получаем строку для парсинга  из входных строк , но с текущим годом
                        
            DateTime datenow = DateTime.Now; // получаем текущую дату
            DateTime dateb = DateTime.Parse(input);// получаем дату рождения в этом году
            int diff;
            if (datenow <= dateb)
            {
                diff = Convert.ToInt32(yearnow) - Convert.ToInt32(year) - 1;
                return "(" + diff + ")";
            }
            diff = Convert.ToInt32(yearnow) - Convert.ToInt32(year); 
            return "(" + diff + ")"; 

        }
        private string CleanUpDates(string day, string month, string year, string prefix)
        {
            if ((day == null || day == "") && (month == null || month == "") && (year == null || year == ""))
            {
                return "";
            }
            return (prefix + CleanUpName(day,".") + CleanUpName(month,"") + CleanUpName(year,"") + Age(year, month, day) + "\r\n");
        }
        
        private string CleanUpPlus(string text)
        {
            if (text== null || text== "")
            {
                return "";
            }
            return (text + "\r\n");
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select g).ToList();
            }
        }

        public int CompareTo(ContactData other)
        {
            if (other is null)
            {
                return 1;
            }
            // если firstname равно, то сравниваем lastname
            if (Firstname.CompareTo(other.Firstname) == 0)
            {
                return Lastname.CompareTo(other.Lastname);
            }
            return Firstname.CompareTo(other.Firstname);
        }

        public bool Equals(ContactData other)
        {
            if (other is null)
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname.Equals(other.Firstname) && Lastname.Equals(other.Lastname);
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }
        public override string ToString()
        {
            return "firstname=" + Firstname + " lastname=" + Lastname;
        }

    }
}
