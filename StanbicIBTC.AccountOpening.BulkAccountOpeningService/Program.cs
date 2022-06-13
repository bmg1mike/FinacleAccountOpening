using StanbicIBTC.AccountOpening.BulkAccountOpeningService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient();
    })
    .Build();

await host.RunAsync();
