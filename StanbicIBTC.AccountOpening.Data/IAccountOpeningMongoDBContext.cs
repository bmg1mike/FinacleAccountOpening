using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Core.Data;

public partial interface IAccountOpeningMongoDBContext
{
    //IMongoCollection<OutboundLog> OutboundLogs { get; set; }
    IMongoCollection<CIFRequest> CIFRequests { get; set; }
    IMongoCollection<CIFNextOfKinDetail> CIFNextOfKinDetails { get; set; }
    IMongoCollection<AccountOpeningAttempt> AccountOpeningAttempts { get; set; }
    IMongoCollection<InboundLog> InboundLogs { get; set; }
    IMongoCollection<OutboundLog> OutboundLogs { get; set; }
    IMongoCollection<BulkAccountRequest> BulkAccountRequests { get; set; }
    IMongoCollection<BulkAccount> BulkAccounts { get; set; }
    IMongoCollection<RMIdentity> RMIdentities { get; set; }

}

