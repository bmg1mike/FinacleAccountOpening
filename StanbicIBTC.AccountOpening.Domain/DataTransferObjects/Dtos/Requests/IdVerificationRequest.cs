namespace StanbicIBTC.AccountOpening.Domain;

public class IdVerificationRequest
{
    public string idNumber { get; set; }
    public IdType idType { get; set; }
    public string lastNameOnId { get; set; }
    public string processingOfficer { get; set; }
    public bool returnImages { get; set; } = false;
}