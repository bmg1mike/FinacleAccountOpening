using System.Xml.Serialization;

namespace StanbicIBTC.AccountOpening.Domain;

public class AccountOpeningModel
{
    [XmlElement(ElementName = "accountNo")]
    public string AccountNo { get; set; }

    [XmlElement(ElementName = "cifId")]
    public string CifId { get; set; }

    [XmlElement(ElementName = "accountType")]
    public string AccountType { get; set; }

    [XmlElement(ElementName = "currency")]
    public string Currency { get; set; }

    [XmlElement(ElementName = "status")]
    public string Status { get; set; }

    [XmlIgnore]
    public string ResponseCode { get; set; }

    [XmlIgnore]
    public string ResponseDescription { get; set; }
}
