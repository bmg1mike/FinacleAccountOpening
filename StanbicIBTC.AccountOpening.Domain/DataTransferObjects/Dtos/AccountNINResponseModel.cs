namespace StanbicIBTC.AccountOpening.Domain;

public class AccountNINResponseModel
{
    public string ResultCode { get; set; }
        public string ResultText { get; set; }
        public string IsFinalResult { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string DOB { get; set; }
        public NINDetail FullData { get; set; }
}
public class NINDetail
{
    public string birthdate { get; set; }
    public string nin { get; set; }
    public string telephoneno { get; set; }
    public string state { get; set; }
    public string othername { get; set; }
    public string nok_firstname { get; set; }
    public string nok_lastname { get; set; }
    public string nok_state { get; set; }
    public string nok_lga { get; set; }
    public string nok_address1 { get; set; }
    public string nok_address2 { get; set; }
    public string nok_town { get; set; }
    public string firstname { get; set; }
    public string surname { get; set; }
    public string middlename { get; set; }
    public string gender { get; set; }
    public string title { get; set; }
}