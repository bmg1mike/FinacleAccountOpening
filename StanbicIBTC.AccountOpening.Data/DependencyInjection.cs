using FluentValidation.AspNetCore;
using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Core.Data;
public static class DependencyInjection
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AccountOpeningAttemptDtoValidator>());
        

        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoDb")));

        services.AddSingleton<IMongoDbConfig, MongoDbConfig>(
            sp => new MongoDbConfig(configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        services.AddScoped<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();


         services.AddTransient<ICIFRequestRepository, CIFRequestRepository>();
         services.AddTransient<ICIFNextOfKinDetailRepository, CIFNextOfKinDetailRepository>();
         services.AddTransient<IAccountOpeningAttemptRepository, AccountOpeningAttemptRepository>();
         services.AddTransient<IInboundLogRepository, InboundLogRepository>();
         services.AddTransient<IOutboundLogRepository, OutboundLogRepository>();


        return services;

    }

}
