using Microsoft.AspNetCore.Builder;
using StanbicIBTC.AccountOpening.Core.Data;
using StanbicIBTC.AccountOpening.WorkerService;
using Serilog;
using StanbicIBTC.AccountOpening.Service;
using StanbicIBTC.AccountOpening.Core.Repositories;
using StanbicIBTC.AccountOpening.Domain.Config;
using MongoDB.Driver;
using StanbicIBTC.AccountOpening.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext,services) =>
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
        services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration["AccountOpeningConnection:ConnectionString"]),ServiceLifetime.Singleton);
        
        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        services.AddHttpClient<ISoapRequestHelper,SoapRequestHelper>();
        services.AddSingleton<IOutboundLogRepository, OutboundLogRepository>();
        services.AddSingleton<IInboundLogRepository, InboundLogRepository>();

        services.AddHostedService<Worker>();
        
    })
    .UseSerilog((context, config) =>
    {
        config.Enrich.FromLogContext()
            .WriteTo.Console()
            .ReadFrom.Configuration(context.Configuration);

    })
    
    .Build();

await host.RunAsync();





