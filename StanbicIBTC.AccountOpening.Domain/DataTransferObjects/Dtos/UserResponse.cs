using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain
{
    public class UserResponse
    {
        public string Bvn { get; set; }
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cif { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
