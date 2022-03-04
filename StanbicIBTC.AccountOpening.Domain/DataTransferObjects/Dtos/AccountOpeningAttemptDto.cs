namespace StanbicIBTC.AccountOpening.Domain;
public partial class AccountOpeningAttemptDto
{
    public string FirstName { get; set;}
    public string LastName { get; set;}
    public string Bvn { get; set;}
    public string Response { get; set;}
    public bool IsSuccessful { get; set;}
    public bool AttemptedDate { get; set;}
    public bool PhoneNumber { get; set;}

}

