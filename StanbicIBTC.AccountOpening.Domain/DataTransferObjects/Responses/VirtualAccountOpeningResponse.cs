using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class VirtualAccountOpeningResponse
{
    public string AccountNumber { get; set; }
    public string AccountStatus { get; set; }
    public string SessionId { get; set; }
    public string RequestId { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseDescription { get; set; }
    public string ResponseFriendlyMessage { get; set; }
}

