var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddHealthChecks()
   .AddUrlGroup(new Uri    
            ("https://www.google.com"),
             name: "Google",
             failureStatus: HealthStatus.Degraded)
    .AddUrlGroup(new Uri    
            ("https://api.remita.net/"),
             name: "Remita",
             failureStatus: HealthStatus.Degraded)
    .AddUrlGroup(new Uri    
            ("https://uat.firstcentralcreditbureau.com/FirstCentralNigeriaWebService_JSON3/FirstCentralNigeriaWebService.asmx"),
             name: "First Central Credit Bureau",
             failureStatus: HealthStatus.Degraded)
;

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));


builder.Services.AddDataDependencies(builder.Configuration);
builder.Services.AddServiceDependencies(builder.Configuration);



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
    opt.AddHealthCheckEndpoint("default api", "/health"); //map health check api    
})    
.AddInMemoryStorage();   

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecksUI();

app.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

app.Run();

