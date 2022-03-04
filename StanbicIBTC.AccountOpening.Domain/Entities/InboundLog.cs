namespace StanbicIBTC.AccountOpening.Domain.Entities;

public partial class InboundLog
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]

   public string InboundLogId { get; set;}
   public string RequestSystem { get; set;}
   public string APICalled { get; set;}
   public string APIMethod { get; set;}
   public DateTime LogDate { get; set;}
   public string ImpactUniqueIdentifier { get; set;}
   public string ImpactUniqueidentifierValue { get; set;}
   public string AlternateUniqueIdentifier { get; set;}
   public string AlternateUniqueidentifierValue { get; set;}
   public string RequestDetails { get; set;}
   public DateTime RequestDateTime { get; set;}
   public string ResponseDetails { get; set;}
   public DateTime ResponseDateTIme { get; set;}
   public List<OutboundLog> OutboundLogs { get; set;}

}

