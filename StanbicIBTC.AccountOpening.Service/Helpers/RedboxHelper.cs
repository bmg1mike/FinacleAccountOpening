using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using StanbicIBTC.AccountOpening.Data;

namespace StanbicIBTC.AccountOpening.Service;

public class RedboxHelper
{
    private readonly ILogger<RedboxHelper> _logger;
    private readonly ModelContext _modelContext;
    private readonly IRestRequestHelper _restRequestHelper;
    private readonly IConfiguration _config;

    public RedboxHelper(ILogger<RedboxHelper> logger, ModelContext modelContext, IRestRequestHelper restRequestHelper, IConfiguration config)
    {
        _logger = logger;
        _modelContext = modelContext;
        _restRequestHelper = restRequestHelper;
        _config = config;
    }

    
}
