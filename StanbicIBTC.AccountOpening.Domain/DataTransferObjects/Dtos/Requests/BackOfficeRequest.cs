namespace StanbicIBTC.AccountOpening.Domain;

public class BackOfficeRequest
{
    public string transRequestId { get; set; } = Guid.NewGuid().ToString();
    public InitiateRequest initiateRequest { get; set; }
    public PersonalInfo personalInfo { get; set; }
    public ContactInfo contactInfo { get; set; }
    public CifnokInfo cifnokInfo { get; set; }
    public CifEmploymentInfo cifEmploymentInfo { get; set; }
    public Documents documents { get; set; }
}

public class CifEmploymentInfo
{
    public string requestTranId { get; set; } = Guid.NewGuid().ToString();
    public string employmentStatus { get; set; }
    public string natureOfBusiness { get; set; }
    public string incomeRange { get; set; }
    public string prominentPubFam { get; set; }
    public string employerName { get; set; }
    public string prominentIntFam { get; set; }
    public string occupation { get; set; }
    public string educationLevel { get; set; }
    public string institute { get; set; }
    public string industry { get; set; }
    public string promIntName { get; set; }
    public string promIntPosition { get; set; }
    public string promIntRelation { get; set; }
    public string promPubName { get; set; }
    public string promPubPosition { get; set; }
    public string promPubRelation { get; set; }
}

public class CifnokInfo
{
    public string requestTranId { get; set; } = Guid.NewGuid().ToString();
    public string nokFullname { get; set; }
    public string nokAddress { get; set; }
    public string nokPhoneNumber { get; set; }
    public string nokEmail { get; set; }
    public string nokRelation { get; set; }
    public string nokDateOfBirth { get; set; }
    public string nokGender { get; set; }
}

public class ContactInfo
{
    public string requestTranId { get; set; }
    public string houseNumber { get; set; }
    public string streetName { get; set; }
    public string nearestBustStop { get; set; }
    public string townCity { get; set; }
    public string contactLGA { get; set; }
    public string contactState { get; set; }
    public string description { get; set; }
    public string aliase { get; set; }
}

public class Documents
{
    public string requestTranId { get; set; }
    public string idType { get; set; }
    public string idNumber { get; set; }
    public string idDoc { get; set; }
    public string idDocExtension { get; set; }
    public string picDoc { get; set; }
    public string picDocExtension { get; set; }
    public string utilityDoc { get; set; }
    public string utilityDocExtension { get; set; }
    public string haveDebitCard { get; set; }
    public string signatureDoc { get; set; }
    public string signatureDocExtension { get; set; }
}

public class InitiateRequest
{
    public string accountNumber { get; set; }
    public string accountName { get; set; }
    public string phoneNumber { get; set; }
    public string email { get; set; }
    public string oldScheme { get; set; }
    public string newScheme { get; set; }
    public string newAddress { get; set; }
    public string bvn { get; set; }
}

public class PersonalInfo
{
    public string requestTranId { get; set; }
    public string placeOfBirth { get; set; }
    public string motherMaidenName { get; set; }
    public string nationality { get; set; }
    public string city { get; set; }
    public string email { get; set; }
    public string gender { get; set; }
    public string homeAddress { get; set; }
}



