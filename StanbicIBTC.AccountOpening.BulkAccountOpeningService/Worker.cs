namespace StanbicIBTC.AccountOpening.BulkAccountOpeningService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IApiCall _apiCall;

    public Worker(ILogger<Worker> logger, IApiCall apiCall)
    {
        _logger = logger;
        _apiCall = apiCall;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Waiting For API To Start");
        Thread.Sleep(10000);

        while (!stoppingToken.IsCancellationRequested)
        {
            var requests = await _apiCall.GetApprovedRequests();
            if (requests.Count > 0)
            {
                foreach (var item in requests)
                {
                    await _apiCall.OpenBulkAccounts(item);
                }
            }
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
