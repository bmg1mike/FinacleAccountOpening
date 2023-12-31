using StanbicIBTC.AccountOpening.Domain.Enums;
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

                
                var requests = await _cifrequestRepository.GetPendingCifRequests();

                while (requests.Count == 0)
                {
                    requests = await _cifrequestRepository.GetPendingCifRequests();
                }



                foreach (var item in requests)
                {
                    var result = string.Empty;
                    if ((item.AccountTypeRequested == AccountTypeRequested.Tier_One.ToString() || item.AccountTypeRequested == AccountTypeRequested.Tier_Three.ToString() || item.AccountTypeRequested == AccountTypeRequested.Bulk_Tier_One.ToString()) && string.IsNullOrEmpty(item.AccountNumber))
                    {
                        result = await _accountOpeningService.OpenAccount(item);
                    }
                    if ((item.AccountTypeRequested == AccountTypeRequested.Tier_Three.ToString() || item.AccountTypeRequested == AccountTypeRequested.Tier_One_Upgrade.ToString()) && !string.IsNullOrEmpty(item.AccountNumber) && item.IsTierThreeAccount == false)
                    {
                        result = await _accountOpeningService.UpgradeToTierThree(item);
                    }
                    _logger.LogInformation($"{result}");
                }

                await Task.Delay(_config.GetValue<int>("WorkerService:Interval"), stoppingToken); // runs every 30 seconds
            }
        }
    }
}