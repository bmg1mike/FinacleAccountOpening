using System;
using System.Collections.Generic;

namespace StanbicIBTC.AccountOpening.Domain
{
    public partial class RbxBpmCorpCifCompany
    {
        public string CompanyId { get; set; } = Guid.NewGuid().ToString();
        public string BusinessAddress { get; set; }
        public string CountryOfIncorporation { get; set; }
        public string PrincipalOfficeCountry { get; set; }
        public string CountryOfRegistration { get; set; }
        public DateTime? DateCreated { get; set; }
        public string HeadOfficeAddress { get; set; }
        public string IsForeignCompany { get; set; }
        public string RegisteredAddress { get; set; }
        public string RegisteredName { get; set; }
        public string RegistrationNumber { get; set; }
        public string TradeName { get; set; }
        public string FkCustomData { get; set; }

        public  RbxBpmCorpCifCustomDatum FkCustomDataNavigation { get; set; }
    }
}
