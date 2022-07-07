using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class ApiResult
{
    public string responseCode { get; set; }
    public string responseDescription { get; set; }
    public object data { get; set; } = null;
}

