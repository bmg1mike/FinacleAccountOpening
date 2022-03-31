using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

    public class TierOneUgrade
    {
        public string AccountNumber { get; set; }
        public decimal  MonthlyIncome { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string IdentityType { get; set; }
        public string IdNumber { get; set; }
        public string IdImage { get; set; }
        public DateTime IdIssueDate { get; set; }
        public DateTime? IdExpiryDate { get; set; }
        public NextOfKin NextOfKin { get; set; }

    }

