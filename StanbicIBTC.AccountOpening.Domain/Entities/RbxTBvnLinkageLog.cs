using System;
using System.Collections.Generic;

namespace StanbicIBTC.AccountOpening.Domain
{
    public partial class RbxTBvnLinkageLog
    {
        public long Id { get; set; } 
        public string AcctNo { get; set; }
        public string AcctName { get; set; }
        public string BankEnrolled { get; set; }
        public string BranchEnrolled { get; set; }
        public string BvnForm { get; set; }
        public DateTime? CreateDate { get; set; }
        public string BvnNumber { get; set; }
        public string BranchNo { get; set; }
        public string BranchName { get; set; }
        public string CifId { get; set; }
        public string PhoneNo { get; set; }
        public string RecordIsFinalFlag { get; set; }
        public string RecordDelFlag { get; set; }
        public string RecordHash { get; set; }
        public string RecordSubmissionFlag { get; set; }
        public string Sapid { get; set; }
        public string TransactionStatus { get; set; }
        public string WkfId { get; set; }
        public string ComponentServerIp { get; set; }
        public DateTime? TranDate { get; set; }
        public DateTime? TranTime { get; set; }
    }
}
