namespace StanbicIBTC.AccountOpening.Domain.Config;

public partial interface IMongoDbConfig
{
      string DatabaseName { get; set; }
      string ConnectionString { get; set; }

}
