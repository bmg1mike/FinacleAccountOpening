
namespace StanbicIBTC.AccountOpening.Service
{
    public interface IMassageNotification
    {
        Task<bool> SendAccountOpeningSMS(SMSRequest request);
    }
}