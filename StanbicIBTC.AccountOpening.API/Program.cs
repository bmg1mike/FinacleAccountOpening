using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StanbicIBTC.AccountOpening.API;
using StanbicIBTC.AccountOpening.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.SetBasePath($"{Directory.GetCurrentDirectory()}/others")
    .AddJsonFile("appsettings.json")
    .Build();

builder.Host.UseSerilog((context, config) =>
{
    config.Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stanbic IBTC Account Opening Service", Version = "v1" });
//     c.AddServer(new OpenApiServer { Url = "https://stanbic.nibse.com/mybank/newaccountopening", Description = "URL for UAT requests" });
//     c.AddServer(new OpenApiServer { Url = "https://localhost:7215", Description = "URL for Https Dev requests" });
//     c.AddServer(new OpenApiServer { Url = "http://localhost:5103", Description = "URL for Http Dev requests" });

// });

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));


builder.Services.AddDataDependencies(builder.Configuration);
builder.Services.AddServiceDependencies(builder.Configuration);
builder.Services.AddIdentityService();


builder.Services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                //x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");  
            });

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
    opt.SetApiMaxActiveRequests(1); //api requests concurrency    
    opt.AddHealthCheckEndpoint("Acount Opening api", $"https://{Dns.GetHostName()}:5100/health"); //map health check api    
})
.AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migraiton");
}

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();

app.UseCors("corsapp");

app.UseHttpsRedirection();

//  app.UseSwagger();
//     app.UseSwaggerUI(c => { 
//                 c.SwaggerEndpoint("./swagger/v1/swagger.json", "AccountOpening.API v1");
//                 c.RoutePrefix = "";
//             });
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecksUI();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

