namespace StanbicIBTC.AccountOpening.Core.Data;

public partial interface IAccountOpeningMongoDBContext
{
    //IMongoCollection<OutboundLog> OutboundLogs { get; set; }
     IMongoCollection<CIFRequest> CIFRequests { get; set; }
     IMongoCollection<CIFNextOfKinDetail> CIFNextOfKinDetails { get; set; }
     IMongoCollection<AccountOpeningAttempt> AccountOpeningAttempts { get; set; }
     IMongoCollection<InboundLog> InboundLogs { get; set; }
     IMongoCollection<OutboundLog> OutboundLogs { get; set; }


}

