using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StanbicIBTC.AccountOpening.Domain;

public class CIFCreationResponse
{
    [XmlElement("SinkTransactionRef")]
    public string SinkTransactionRef { get; set; }
    [XmlElement("ResponseCode")]
    public string ResponseCode { get; set; }
    [XmlElement("ResponseStatus")]
    public string ResponseStatus { get; set; }
    [XmlElement("ResponseDescription")]
    public string ResponseDescription { get; set; }
    [XmlElement("CustomerId")]
    public string CustomerId { get; set; }
    [XmlElement("CustomerCoreProfileActivationStatus")]
    public string CustomerCoreProfileActivationStatus { get; set; }
}
