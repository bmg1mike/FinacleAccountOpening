namespace StanbicIBTC.AccountOpening.Domain.Entities;

public partial class CIFRequest
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string CIFRequestId { get; set;}
   public string FirstName { get; set;}
   public string LastName { get; set;}
   public string MiddleName { get; set;}
   public string Email { get; set;}
   public string PhoneNumber { get; set;}
   public string CustomerAddress { get; set;}
   public string CustomerBVN { get; set;}
   public string DateOfBirthInY_M_D_Format { get; set;}
   public string Gender { get; set;}
   public string StateOfResidence { get; set;}
   public string LgaOfResidence { get; set;}
   public string Title { get; set;}
   public string BvnErollmentBank { get; set;}
   public string BvnEnrollmentBranch { get; set;}
   public string OccupationCode { get; set;}
   public string EmploymentStatus { get; set;}
   public string NIN { get; set;}
   public string MaritalStatus { get; set;}
   public CIFNextOfKinDetail NextOfKinDetail { get; set;}
   public string AccountOpeningStatus { get; set;}
   public string DateCreated { get; set;} = DateOnly.FromDateTime(DateTime.Now).ToString();
   public string DateModified { get; set;} = DateOnly.FromDateTime(DateTime.Now).ToString();
   public string TimeCreated { get; set; } = TimeOnly.FromDateTime(DateTime.Now).ToString();
   public string TimeModified { get; set; } = TimeOnly.FromDateTime(DateTime.Now).ToString();
   public string Platform { get; set;}

}

