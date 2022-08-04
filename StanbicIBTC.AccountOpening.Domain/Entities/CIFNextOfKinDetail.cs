namespace StanbicIBTC.AccountOpening.Domain.Entities;

public partial class CIFNextOfKinDetail
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string CIFNextOfKinDetailId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string DateOfBirthInY_M_DFormat { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string State { get; set; }
    public string Town { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string DateCreated { get; set; }
    public string DateModified { get; set; }
    public string Relationship { get; set; }
    public string Gender { get; set; }
}

