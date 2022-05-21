using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StanbicIBTC.AccountOpening.Core.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value));

        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        services.AddHealthChecks()
                    .AddMongoDb(configuration.GetSection("MongoDbSettings:ConnectionString").Value, "MongoDb Health", HealthStatus.Degraded);
        services.AddHealthChecks()
                    .AddOracle(configuration.GetConnectionString("FinacleConnection"),"select * from v$version","Finacle Db Health",HealthStatus.Degraded);
        // services.AddHealthChecks()
        //             .AddOracle(configuration.GetConnectionString("AccountOpeningConnection"),"select * from v$version","RedBox Db Health",HealthStatus.Degraded);
        

        services.AddScoped<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();


         services.AddTransient<ICIFRequestService, CIFRequestService>();
         services.AddTransient<ICIFNextOfKinDetailService, CIFNextOfKinDetailService>();
         services.AddTransient<IAccountOpeningAttemptService, AccountOpeningAttemptService>();
         services.AddTransient<IInboundLogService, InboundLogService>();
         services.AddTransient<IOutboundLogService, OutboundLogService>();
         services.AddScoped<IAccountOpeningService,AccountOpeningService>();
         services.AddScoped<IRestRequestHelper,RestRequestHelper>();
         services.AddScoped<ISoapRequestHelper,SoapRequestHelper>();
         services.AddScoped<ISmsNotification, SmsNotification>();
         services.AddHttpClient<ISoapRequestHelper,SoapRequestHelper>();
            

        return services;

    }

}
