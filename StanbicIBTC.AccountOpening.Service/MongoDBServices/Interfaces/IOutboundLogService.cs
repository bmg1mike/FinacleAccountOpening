namespace StanbicIBTC.AccountOpening.Core.Services;

public partial interface IOutboundLogService
{
    Task<List<OutboundLog>> GetOutboundLogs();
    Task<OutboundLog>  GetOutboundLog(string id);
    Task<string> CreateOutboundLog(OutboundLog outboundLog);
    Task<bool> UpdateOutboundLog(string id, OutboundLog outboundLog);
    Task<bool> RemoveOutboundLog(string id);
}
