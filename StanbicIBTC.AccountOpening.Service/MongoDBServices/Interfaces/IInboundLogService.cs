namespace StanbicIBTC.AccountOpening.Core.Services;

public partial interface IInboundLogService
{
    Task<List<InboundLog>> GetInboundLogs();
    Task<InboundLog>  GetInboundLog(string id);
    Task<string> CreateInboundLog(InboundLog inboundLog);
    Task<bool> UpdateInboundLog(string id, InboundLog inboundLog);
    Task<bool> RemoveInboundLog(string id);
}
