using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain.DataTransferObjects.Dtos.Requests
{
    public class AccountOpeningDocuments
    {
        public string Signature { get; set; }
        public string IDCard { get; set; }
        public int MyProperty { get; set; }
    }
}
