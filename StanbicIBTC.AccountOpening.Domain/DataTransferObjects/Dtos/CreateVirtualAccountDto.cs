namespace StanbicIBTC.AccountOpening.Domain;

public class CreateVirtualAccountDto
{
    public string PhoneNumber { get; set; }
    public string SecretWord { get; set; }
    public string BankVerificationNumber { get; set; }
    public string ReferralCode { get; set; }
}

