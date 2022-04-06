using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class MobileWebOnboardingResponse
{
    public bool IsSuccess { get; set; }
    public string Value { get; set; }
    public string Error { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseDescription { get; set; }
}

