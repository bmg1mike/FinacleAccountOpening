namespace StanbicIBTC.AccountOpening.Domain;

public class CreateVirtualAccountDto
{
    public string PhoneNumber { get; set; }
    public string SecretWord { get; set; }
    public string BankVerificationNumber { get; set; }
    public string ReferralCode { get; set; }
     public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
}

