using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Serilog;
using StanbicIBTC.AccountOpening.BulkAccountOpeningService;
using StanbicIBTC.AccountOpening.Core.Data;
using StanbicIBTC.AccountOpening.Core.Repositories;
using StanbicIBTC.AccountOpening.Data;
using StanbicIBTC.AccountOpening.Domain.Config;
using StanbicIBTC.AccountOpening.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        
        services.AddHttpClient<IApiCall,ApiCall>();
        services.AddSingleton<IAccountOpeningService, AccountOpeningService>();
        services.AddSingleton<IBulkAccountOpeningService, BulkAccountOpeningService>();
        services.AddSingleton<IBulkAccountRequestRepository, BulkAccountRequestRepository>();
        services.AddSingleton<ICIFRequestRepository, CIFRequestRepository>();
        services.AddSingleton<ISoapRequestHelper, SoapRequestHelper>();
        services.AddSingleton<IRestRequestHelper, RestRequestHelper>();
        services.AddSingleton<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();
        services.AddSingleton<IAccountOpeningAttemptRepository, AccountOpeningAttemptRepository>();
        services.AddSingleton<IFinacleRepository, FinacleRepository>();
        services.AddSingleton<IMassageNotification, MassageNotification>();
        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value));
        services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration["AccountOpeningConnection:ConnectionString"]), ServiceLifetime.Singleton);

        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        services.AddHttpClient<ISoapRequestHelper, SoapRequestHelper>();
        services.AddSingleton<IOutboundLogRepository, OutboundLogRepository>();
        services.AddSingleton<IInboundLogRepository, InboundLogRepository>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
