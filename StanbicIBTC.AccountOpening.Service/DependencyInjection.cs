using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;

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
                    .AddOracle(configuration.GetConnectionString("FinacleConnection"), "select * from v$version", "Finacle Db Health", HealthStatus.Degraded);
        services.AddHealthChecks()
            .AddSqlServer(configuration["AccountOpeningConnection:ConnectionString"], null, "SQLServer Health", HealthStatus.Degraded);
        //services.AddHealthChecks()
        //             .AddOracle(configuration.GetConnectionString("AccountOpeningConnection"),"select * from v$version","RedBox Db Health",HealthStatus.Degraded);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IAccountOpeningMongoDBContext, AccountOpeningMongoDBContext>();


        services.AddTransient<ICIFRequestService, CIFRequestService>();
        services.AddTransient<ICIFNextOfKinDetailService, CIFNextOfKinDetailService>();
        services.AddTransient<IAccountOpeningAttemptService, AccountOpeningAttemptService>();
        services.AddTransient<IInboundLogService, InboundLogService>();
        services.AddTransient<IOutboundLogService, OutboundLogService>();
        services.AddScoped<IAccountOpeningService, AccountOpeningService>();
        services.AddTransient<IBulkAccountOpeningService, BulkAccountOpeningService>();
        services.AddScoped<IRestRequestHelper, RestRequestHelper>();
        services.AddScoped<ISoapRequestHelper, SoapRequestHelper>();
        services.AddScoped<IMassageNotification, MassageNotification>();
        services.AddHttpClient<ISoapRequestHelper, SoapRequestHelper>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x => {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSecretKey"])),
                ValidateIssuer = true,
                ValidIssuer = configuration["Token_Issuer"],
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            x.Authority = configuration["Token_Issuer"];
        });


        return services;

    }

}
