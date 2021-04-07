using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    public class ContactData : IComparable<ContactData>, IEquatable<ContactData>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int CompareTo(ContactData other)
        {
           if (this.FirstName.CompareTo(other.FirstName) != 0)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }
           else if (this.MiddleName.CompareTo(other.MiddleName) != 0)
            {
                return this.MiddleName.CompareTo(other.MiddleName);
            }
           else
            {
                return this.LastName.CompareTo(other.LastName);
            }
        }

        public bool Equals(ContactData other)
        {
            if (this.FirstName.Equals(other.FirstName))
            {
                return this.FirstName.Equals(other.FirstName);
            }
            else if (this.MiddleName.Equals(other.MiddleName))
            {
                return this.MiddleName.Equals(other.MiddleName);
            }
            else
            {
                return this.LastName.Equals(other.LastName);
            }
        }
    }
}
