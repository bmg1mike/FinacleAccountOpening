using StanbicIBTC.AccountOpening.Service;

namespace StanbicIBTC.AccountOpening.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IAccountOpeningService _accountOpeningService;
        private readonly ICIFRequestRepository _cifrequestRepository;

        public Worker(ILogger<Worker> logger, IAccountOpeningService accountOpeningService, IConfiguration config, ICIFRequestRepository cifrequestRepository)
        {
            _logger = logger;
            _accountOpeningService = accountOpeningService;
            _config = config;
            _cifrequestRepository = cifrequestRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Waiting For API To Start");
            Thread.Sleep(10000);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //CheckCifRequest:
                var requests = await _cifrequestRepository.GetPendingCifRequests();

                while(requests.Count == 0)
                {
                    requests = await _cifrequestRepository.GetPendingCifRequests();
                }

                // if (requests.Count == 0)
                // {
                //     Thread.Sleep(10000);
                //     goto CheckCifRequest;
                // }

                foreach (var item in requests)
                {
                    var result = await _accountOpeningService.OpenAccount(item);
                    _logger.LogInformation($"{result}");
                }

                await Task.Delay(_config.GetValue<int>("WorkerService:Interval"), stoppingToken); // runs every 30 seconds
            }
        }
    }
}