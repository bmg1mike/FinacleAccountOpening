namespace StanbicIBTC.AccountOpening.Domain;

public class RbxTFinCustCreationLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SrcModuleId { get; set; }
    public string LifeTimeId { get; set; }
    public string SrcTranRef { get; set; }
    public string ChannelId { get; set; }
    public string RequestUuid { get; set; }
    public string ServiceRequestId { get; set; }
    public string Salutation { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Bvn { get; set; }
    public string BvnEnrollmentBank { get; set; }
    public string BvnEnrollmentBranch { get; set; }
    public string BvnLinkageFlag { get; set; }
    public string PhysicalAddressDetails { get; set; }
    public string DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string OccupationCode { get; set; }
    public int? IsStaffFlag { get; set; }
    public string StaffEmployeeId { get; set; }
    public string SegmentationClass { get; set; }
    public string SubSegment { get; set; }
    public string AccountManager { get; set; }
    public string LanguageCodes { get; set; }
    public string RelationshipOpeningDate { get; set; }
    public string TaxDeductionTable { get; set; }
    public string MaritalStatus { get; set; }
    public string Nationality { get; set; }
    public string EmploymentStatus { get; set; }
    public string SupportingDocumentData { get; set; }
    public string CustomerTypeCode { get; set; }
    public int? IsCustomerNonResident { get; set; }
    public string CustomerBranchId { get; set; }
    public string CustomerId { get; set; }
    public string Entity { get; set; }
    public string CustCoreMigStatus { get; set; }
    public string CustCoreMigDesc { get; set; }
    public string RespCode { get; set; }
    public string RespDesc { get; set; }
    public string RespStatus { get; set; }
    public string FinRespCode { get; set; }
    public string FinRespDesc { get; set; }
    public string FinRespStatus { get; set; }
    public string FinErrorSrc { get; set; }
    public string FinErrorType { get; set; }
    public DateTime? ProdReqInTime { get; set; }
    public DateTime? ProdReqOutTime { get; set; }
    public DateTime? ProdRespInTime { get; set; }
    public DateTime? ProdRespOutTime { get; set; }
    public string ComponentServerIp { get; set; }
    public DateTime? TranDate { get; set; }
    public DateTime? TranTime { get; set; }
}