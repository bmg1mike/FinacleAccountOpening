namespace StanbicIBTC.AccountOpening.Domain.Entities;

public partial class AccountOpeningAttempt
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]

   public string AccountOpeningAttemptId { get; set;}
   public string FirstName { get; set;}
   public string LastName { get; set;}
   public string Bvn { get; set;}
   public string Response { get; set;}
   public bool IsSuccessful { get; set;}
   public bool AttemptedDate { get; set;}
   public string PhoneNumber { get; set;}

}

