var builder = WebApplication.CreateBuilder(args);
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IAccountOpeningService,AccountOpeningService>();
        services.AddSingleton<ICIFRequestRepository, CIFRequestRepository>();
        services.AddSingleton<ISoapRequestHelper, SoapRequestHelper>();
        services.AddSingleton<IRestRequestHelper, RestRequestHelper>();
        services.AddSingleton<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();
        services.AddSingleton<IAccountOpeningAttemptRepository, AccountOpeningAttemptRepository>();
        services.AddSingleton<IFinacleRepository, FinacleRepository>();
        services.AddSingleton<ISmsNotification, SmsNotification>();
        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value));
        services.AddDbContext<DataContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("RedBoxConnection")),ServiceLifetime.Singleton);
        
        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
