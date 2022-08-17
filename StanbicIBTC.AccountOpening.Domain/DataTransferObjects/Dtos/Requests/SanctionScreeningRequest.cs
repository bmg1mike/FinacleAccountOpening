namespace StanbicIBTC.AccountOpening.Domain;

public class SanctionScreeningRequest
{
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateLastModified { get; set; } = DateTime.Now;
    public string CustomerBvn { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerFullHomeAddress { get; set; }
    public string CustomerLastName { get; set; }
    public int? HasBeenTreatedByBot { get; set; }
    public string AccountOpeningRequestId { get; set; } = Guid.NewGuid().ToString();
    public string CustomerMiddleName { get; set; }
}
