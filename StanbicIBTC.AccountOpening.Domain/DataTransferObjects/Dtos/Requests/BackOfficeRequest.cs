namespace StanbicIBTC.AccountOpening.Domain;

public  class BackOfficeRequest
{
    public string RequestTranId { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public string IdDoc { get; set; }
    public string IdDocExtension { get; set; }
    public string PicDoc { get; set; }
    public string PicDocExtension { get; set; }
    public string UtilityDoc { get; set; }
    public string UtilityDocExtension { get; set; }
    public string HaveDebitCard { get; set; }
    public string SignatureDoc { get; set; }
    public string SignatureDocExtension { get; set; }
}
