namespace StanbicIBTC.AccountOpening.Core.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoDb")));

        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        services.AddScoped<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();


         services.AddTransient<ICIFRequestService, CIFRequestService>();
         services.AddTransient<ICIFNextOfKinDetailService, CIFNextOfKinDetailService>();
         services.AddTransient<IAccountOpeningAttemptService, AccountOpeningAttemptService>();
         services.AddTransient<IInboundLogService, InboundLogService>();
         services.AddTransient<IOutboundLogService, OutboundLogService>();


        return services;

    }

}
