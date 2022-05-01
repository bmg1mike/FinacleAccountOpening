using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class CurrentAccountRequest
{
    public string Bvn { get; set; }
    public NextOfKin NextOfKin { get; set; }
    public string FirstReference { get; set; }
    public string SecondReference { get; set; }
    public string AccountNumber { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string OccupationCode { get; set; }
    public object Platform { get; set; }
}

