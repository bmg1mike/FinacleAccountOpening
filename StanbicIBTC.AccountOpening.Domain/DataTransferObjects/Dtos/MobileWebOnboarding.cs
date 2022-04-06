namespace StanbicIBTC.AccountOpening.Domain
{
    public class MobileWebOnboarding
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public string Email { get; set; }
    }
}