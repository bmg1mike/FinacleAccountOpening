namespace StanbicIBTC.AccountOpening.Domain;

public class IdVerificationResponse
{
    public string responseCode { get; set; }
    public string description { get; set; }
    public Detail details { get; set; }
    public bool consent { get; set; }
    public bool isConsent { get; set; }
}

public class Detail 
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string dateOfBirth { get; set; }
    public string status { get; set; }
    public bool dataValidation { get; set; }
    public bool selfieValidation { get; set; }
    public string middleName { get; set; }
    public string image { get; set; }
    public string mobile { get; set; }
    public string birthState { get; set; }
    public string nokState { get; set; }
    public string religion { get; set; }
    public string birthLGA { get; set; }
    public string birthCountry { get; set; }
    public string idNumber { get; set; }
    public string businessId { get; set; }
    public string type { get; set; }
    public string gender { get; set; }
    public string requestedAt { get; set; }
    public string country { get; set; }
    public string createdAt { get; set; }
    public string lastModifiedAt { get; set; }
    public string id { get; set; }
    public RequestedBy requestedBy { get; set; }

}

public class Address
{
    public string town { get; set; }
    public string lga { get; set; }
    public string state { get; set; }
    public string addressLine { get; set; }
}

public class RequestedBy
{
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string middleName { get; set; } = string.Empty;
    public string id { get; set; } = string.Empty;
}