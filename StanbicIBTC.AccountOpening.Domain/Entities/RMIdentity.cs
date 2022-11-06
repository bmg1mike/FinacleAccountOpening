namespace StanbicIBTC.AccountOpening.Domain.Entities;

public class RMIdentity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string RMIdentityId { get; set; }
    public string AANumber { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string SAP { get; set; }
    public string Region { get; set; }
}