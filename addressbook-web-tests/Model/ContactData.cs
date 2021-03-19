using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        private string allPhones;
        private string allEmails;
        private string detailsInfo;
        private string bday;
        private string bmonth;
        private string aday;
        private string amonth;

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

        public string Firstname { get; set; }
        
        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string Nickname { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Homepage { get; set; }

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

        public string Byear { get; set; }

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

        public string Ayear { get; set; }

        public string SecAdress { get; set; }

        public string SecHomePhone { get; set; }

        public string Notes { get; set; }

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
                return "";
            }
            if (day == "" || day == "0")
            {
                day = "1";
            }
            if (month == "" || month == "-")
            {
                month = "1";
            }
            string yearnow = DateTime.Now.Year.ToString(); // получаем текущий год
            string input = yearnow + "/" + month + "/" + day; // получаем строку для парсинга  из входных строк , но с текущим годом
            int diff = 0;
            
            DateTime datenow = DateTime.Now; // получаем текущую дату
            DateTime dateb = DateTime.Parse(input);// получаем дату рождения в этом году
            
            
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

        public string Id { get; set; }
        

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
