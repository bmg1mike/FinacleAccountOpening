using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Core.Data;

public partial class AccountOpeningMongoDBContext : IAccountOpeningMongoDBContext
{
    public IMongoCollection<CIFRequest> CIFRequests { get; set; }
    public IMongoCollection<CIFNextOfKinDetail> CIFNextOfKinDetails { get; set; }
    public IMongoCollection<AccountOpeningAttempt> AccountOpeningAttempts { get; set; }
    public IMongoCollection<InboundLog> InboundLogs { get; set; }
    public IMongoCollection<OutboundLog> OutboundLogs { get; set; }
    public IMongoCollection<BulkAccountRequest> BulkAccountRequests { get; set; }
    public IMongoCollection<BulkAccount> BulkAccounts { get; set; }
    public IMongoCollection<RMIdentity> RMIdentities { get; set; }

    public AccountOpeningMongoDBContext(IMongoDbConfig config, IMongoClient mongoClient)
    {
        var client = new MongoClient(config.ConnectionString);
        var database = client.GetDatabase(config.DatabaseName);

        CIFRequests = database.GetCollection<CIFRequest>("CIF_Requests");
        CIFNextOfKinDetails = database.GetCollection<CIFNextOfKinDetail>("CIF_Next_Of_Kin_Details");
        AccountOpeningAttempts = database.GetCollection<AccountOpeningAttempt>("Account_Opening_Attempts");
        InboundLogs = database.GetCollection<InboundLog>("Inbound_Logs");
        OutboundLogs = database.GetCollection<OutboundLog>("Outbound_Logs");
        BulkAccountRequests = database.GetCollection<BulkAccountRequest>("BulkAccountRequests");
        BulkAccounts = database.GetCollection<BulkAccount>("BulkAccounts");
        RMIdentities = database.GetCollection<RMIdentity>("RM_Identities");

    }

}

