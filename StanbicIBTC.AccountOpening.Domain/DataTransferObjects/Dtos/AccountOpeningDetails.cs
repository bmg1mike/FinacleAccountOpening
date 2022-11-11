namespace StanbicIBTC.AccountOpening.Domain;

public class AccountOpeningDetails
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string CustomerBVN { get; set; }
    public string AccountNumber { get; set; }
    public string Cif { get; set; }
    public RequiredDocuments RequiredDocuments { get; set; }
}