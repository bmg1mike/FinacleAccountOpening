using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public  class FinacleAccountDetailResponse
{
    public string Cif { get; set; }
    public string SchemeCode { get; set; }
    public string AccountName { get; set; }
    public string GlSubHeadCode { get; set; }
}

