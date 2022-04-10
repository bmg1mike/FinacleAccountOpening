using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class VirtualAccountOpeningRequest
{
    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public string SecretWord { get; set; }
    public string BankVerificationNumber { get; set; }
    public string Address { get; set; }
    public string ReferralCode { get; set; } = "HPP00";
    public string AccountType { get; set; } = "WALLET";
    public string RequestId { get; set; }
    public string SessionId { get; set; }
    public int SendWelcomeSMS { get; set; } = 1;
    public int SendWelcomeEmail { get; set; } = 1;
    public int EnableTransactionAlert { get; set; } = 1;
    public int ValidateMaxAccountName { get; set; } = 1;
}
