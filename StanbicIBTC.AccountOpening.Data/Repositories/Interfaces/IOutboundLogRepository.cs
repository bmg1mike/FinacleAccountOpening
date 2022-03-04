namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial interface IOutboundLogRepository
{
    Task<List<OutboundLog>> GetOutboundLogs();
    Task<OutboundLog>  GetOutboundLog(string id);
    Task<string> CreateOutboundLog(OutboundLog outboundLog);
    Task<bool> UpdateOutboundLog(string id, OutboundLog outboundLog);
    Task<bool> RemoveOutboundLog(string id);

    //Task<List<OutboundLog>> GetByFieldName(string fieldName) --Template
    //Task<bool> UpdateSpecificFields(string outboundLogId, OutboundLog outboundLog) --Template

}
