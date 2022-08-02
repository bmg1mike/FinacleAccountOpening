namespace StanbicIBTC.AccountOpening.Domain.Entities;

public partial class CIFRequest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CIFRequestId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerBVN { get; set; }
    public string DateOfBirthInY_M_D_Format { get; set; }
    public string Gender { get; set; }
    public string StateOfResidence { get; set; }
    public string LgaOfResidence { get; set; }
    public string Title { get; set; }
    public string BvnErollmentBank { get; set; }
    public string BvnEnrollmentBranch { get; set; }
    public string OccupationCode { get; set; }
    public string EmploymentStatus { get; set; }
    public string NIN { get; set; }
    public string MaritalStatus { get; set; }
    public CIFNextOfKinDetail NextOfKinDetail { get; set; }
    public string AccountOpeningStatus { get; set; }
    public string DateCreated { get; set; } = DateTime.Now.ToString();
    public string DateModified { get; set; } = DateTime.Now.ToString();
    public string Platform { get; set; }
    public string AccountOpeningAttemptId { get; set; }
    public bool WillOnBoard { get; set; } = false;
    public string SecretQuestion { get; set; }
    public string SecretAnswer { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public decimal MonthlyIncome { get; set; }
    public string EmployerName { get; set; }
    public string EmployerAddress { get; set; }
    public string IdentityType { get; set; }
    public string IdNumber { get; set; }
    public DateTime IdIssueDate { get; set; }
    public DateTime? IdExpiryDate { get; set; }
    public string AccountNumber { get; set; }
    public string AccountTypeRequested { get; set; }
    public bool AddressVerified { get; set; } = false;
    public bool SanctionScreeningVerified { get; set; } = false;
    public string Cif { get; set; }
    public string Response { get; set; }
    public string SanctionScreeningAccountId { get; set; }
    public string AddressverificationId { get; set; }
    public bool IsAccountOpenedSuccessfully { get; set; } = false;
    public bool IsTierThreeAccount { get; set; } = false;
    public bool IsKycDocumentsUploaded { get; set; } = false;
    public bool IsSanctionScreeningLogged { get; set; } = false;
    public bool IsAddressVerificationLogged { get; set; } = false;
    public string SolId { get; set; }
    public string Category { get; set; }
    public string BranchManagerSapId { get; set; }
    public string AccountManagerSapId { get; set; }
    public string UploadedBy { get; set; }
    public RequiredDocuments RequiredDocuments { get; set; }
    public SoleProprietor SoleProprietor { get; set; }
    public DomiciliaryAccountDetails DomiciliaryAccountDetails { get; set; }
    public PoliticallyExposedPersonDetails PoliticallyExposedPersonDetails { get; set; }
    public NonNigerian NonNigerian { get; set; }
    public EmployedAndStudentCustomerInformation EmployedAndStudentCustomerInformation { get; set; }
    public string PoliticallyExposedStatus { get; set; }
    public bool IsBackOfficeLogged { get; set; }
    public string ReasonForFailure { get; set; }
    public string BulkAccountId { get; set; }
}

