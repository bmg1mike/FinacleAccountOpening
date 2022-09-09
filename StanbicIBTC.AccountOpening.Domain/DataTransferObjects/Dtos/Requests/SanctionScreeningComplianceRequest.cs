namespace StanbicIBTC.AccountOpening.Domain;

public class SanctionScreeningComplianceRequest
{
    public string ComplianceApprovalStatus { get; set; } = ApprovalStatus.Pending.ToString();
    public string ComplianceOfficerSapId { get; set; }
    public string Comment { get; set; }
}
