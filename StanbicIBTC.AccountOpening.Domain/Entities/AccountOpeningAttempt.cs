namespace StanbicIBTC.AccountOpening.Domain.Entities;

public partial class AccountOpeningAttempt
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]

   public string AccountOpeningAttemptId { get; set;}
   public string Bvn { get; set;}
   public string Response { get; set;}
   public bool IsSuccessful { get; set;} = false;
   public DateTime AttemptedDate { get; set;} = DateTime.Now;
   public string Cif { get; set; }
   public string AccountNumber { get; set; }
   public string PhoneNumber { get; set;}
    public string Address { get; set; }
   public string AccountTypeRequested { get; set; }
   public bool AddressVerified { get; set; } = false;
   public bool SanctionScreeningVerified { get; set; } = false;
   public string Platform { get; set; }
   
}

