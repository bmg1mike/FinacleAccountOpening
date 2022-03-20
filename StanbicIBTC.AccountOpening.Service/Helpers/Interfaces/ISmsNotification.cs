
namespace StanbicIBTC.AccountOpening.Service
{
    public interface ISmsNotification
    {
        Task SendAccountOpeningSMS(string phoneNumber, string message);
    }
}