using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class SoapCallResponse
{
    public string ResponseCode { get; set; }
    public string ResponseDescription { get; set; }
    public string Details { get; set; }

    public SoapCallResponse()
    {

    }

    public SoapCallResponse(string responseCode,string responseDescription)
    {
        ResponseCode = responseCode;
        ResponseDescription = responseDescription;
    }
}
