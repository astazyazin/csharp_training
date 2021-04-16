using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData : IEquatable<AccountData>, IComparable<AccountData>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }

        public int CompareTo(AccountData other)
        {
            if (other is null)
            {
                return 1;
            }
            
            return Name.CompareTo(other.Name);
        }

        public bool Equals(AccountData other)
        {
            if (other is null)
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name.Equals(other.Name) ;
        }
    }
}
