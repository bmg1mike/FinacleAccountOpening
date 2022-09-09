namespace StanbicIBTC.AccountOpening.Domain;

public class TierThreeAccountOpeningRequest
{
    public string Bvn { get; set; }
    // Contact Details
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ResidenceAddress { get; set; }
    public string NearestBusStop { get; set; }
    public string StateOfResidence { get; set; }
    public string MaritalStatus { get; set; }
    public string NatureOfBusiness { get; set; }
    public string CurrencyCode { get; set; } // For Domiciliary Accounts
    public SoleProprietor SoleProprietor { get; set; }
    public string Nationality { get; set; }
    public NextOfKin NextOfKinDetails { get; set; }
    public Platform Platform { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string OccupationCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public RequiredDocuments RequiredDocuments { get; set; }
    public bool PoliticallyExposed { get; set; }
    public NonNigerian NonNigerian { get; set; }
    public EmployedAndStudentCustomerInformation EmployedAndStudentCustomerInformation { get; set; }
    public PoliticallyExposedPersonDetails PoliticallyExposedPersonDetails { get; set; }
    public DomiciliaryAccountDetails DomiciliaryAccountDetails { get; set; }
    public DateTime? AvrVisitDate { get; set; }
    public string AvrComment { get; set; }
    public string CustomerRelationshipManager { get; set; }
    public string AccountManager { get; set; }
    public string BranchId { get; set; }
    public string Title { get; set; }
    public string CountryOfBirth { get; set; }
    public string SectorCode { get; set; }
    public string SubSectorCode { get; set; }
    public string MotherMaidenName { get; set; }
    public string PurposeOfAccount { get; set; }
    public string SubSegment { get; set; }
    public bool? InternetBankingForRM { get; set; }
    public string LgaOfResidence { get; set; }
}

public class PoliticallyExposedPersonDetails
{
    public string FamilyMemberHeldGovernment { get; set; }
    public string FamilyMemberFullName { get; set; }
    public string FamilyMemberPositionHeld { get; set; }
    public string RelationWithFamilyMember { get; set; }
}

public class NonNigerian
{
    public string ResidencePermitNumber { get; set; }
    public DateTime? PermitIssueDate { get; set; }
    public DateTime? PermitExpiryDate { get; set; }
}

public class EmployedAndStudentCustomerInformation
{
    public DateTime? DateOfEmployment { get; set; }
    public string EmployerName { get; set; }
    public string EmployerAddress { get; set; }
    public decimal MonthlyIncome { get; set; }
    public string InstitutionName { get; set; }
    public string Sector { get; set; }

}

public class RequiredDocuments
{
    public string IdentityType { get; set; }
    public string IdNumber { get; set; }
    public string IdImage { get; set; } // regulatory Id
    public string IdImageExtension { get; set; }
    public DateTime? IdIssueDate { get; set; }
    public DateTime? IdExpiryDate { get; set; }
    public string PassportPhotograph { get; set; }
    public string PassportPhotographExtension { get; set; }
    public string Signature { get; set; }
    public string SignatureExtension { get; set; }
    public string UtilityBill { get; set; }
    public string UtilityBillExtension { get; set; }
    public string AopDocument { get; set; }
    public string BvnVerificationDocument { get; set; }
    public string IdVerificationDocument { get; set; }
    public string SanctionScreeningReportDocument { get; set; }
}

public class SoleProprietor
{
    public string TaxIdentificationNumber { get; set; }
}

public class DomiciliaryAccountDetails
{
    public string SourceOfFunds { get; set; }
    public decimal? ExpectedCumulativeBalance { get; set; }
}
