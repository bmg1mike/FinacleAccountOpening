using Microsoft.AspNetCore.Builder;
using StanbicIBTC.AccountOpening.Core.Data;
using StanbicIBTC.AccountOpening.Core.Services;
using StanbicIBTC.AccountOpening.WorkerService;
using Serilog;
using StanbicIBTC.AccountOpening.Service;
using StanbicIBTC.AccountOpening.Core.Repositories;
using StanbicIBTC.AccountOpening.Domain.Config;
using MongoDB.Driver;
using System.Configuration;
using StanbicIBTC.AccountOpening.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {

        //using (var scope = host.Services.CreateScope())
        //{
        //    var service = scope.ServiceProvider;
        //    try
        //    {
        //        var context = service.GetRequiredService<ModelContext>();
        //        context.Database.EnsureCreated();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        services.AddSingleton<IAccountOpeningService,AccountOpeningService>();
        services.AddSingleton<ICIFRequestRepository, CIFRequestRepository>();
        services.AddSingleton<ISoapRequestHelper, SoapRequestHelper>();
        services.AddSingleton<IRestRequestHelper, RestRequestHelper>();
        services.AddSingleton<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();
        services.AddSingleton<IAccountOpeningAttemptRepository, AccountOpeningAttemptRepository>();
        services.AddSingleton<IFinacleRepository, FinacleRepository>();
        services.AddSingleton<ISmsNotification, SmsNotification>();
        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value));
        services.AddDbContext<ModelContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("RedBoxConnection")),ServiceLifetime.Singleton);
        
        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        //var optionBuilder = new DbContextOptionsBuilder<ModelContext>();
        //optionBuilder.UseOracle(builder.Configuration.GetConnectionString("RedBoxConnection"));

        //services.AddScoped<ModelContext>(d => new ModelContext(optionBuilder.Options));

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





