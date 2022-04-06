using System;
using System.Collections.Generic;

namespace StanbicIBTC.AccountOpening.API.Entities
{
    public partial class RbxBpmCifCustomDatum
    {
        public RbxBpmCifCustomDatum()
        {
            RbxBpmNextOfKinDetails = new HashSet<RbxBpmNextOfKinDetail>();
        }

        public long Id { get; set; }
        public string? AccomodationType { get; set; }
        public string? AssetClassification { get; set; }
        public string? Bvn { get; set; }
        public string? CounterPartyInfo { get; set; }
        public string? CountryOfBirth { get; set; }
        public string? CountryOfTaxResidence { get; set; }
        public string? CustomerId { get; set; }
        public string? EmploymentType { get; set; }
        public string? FnrClassification { get; set; }
        public string? FnrStatus { get; set; }
        public string? ForeignCustomer { get; set; }
        public string? IdentityType { get; set; }
        public string? IndustrySarbCode { get; set; }
        public string? IsOnlyNationality { get; set; }
        public string? KeyContactPerson { get; set; }
        public string? LegalChallengeStatus { get; set; }
        public string? LegalEntity { get; set; }
        public string? LocalIndicia { get; set; }
        public string? MaritalStatus { get; set; }
        public string? MonthlyNetIncome { get; set; }
        public string? Occupation { get; set; }
        public string? PoliticallyExposed { get; set; }
        public string? PreferredName { get; set; }
        public string? PriCountryOfTaxResidence { get; set; }
        public string? PrimaryNationality { get; set; }
        public string? PrimarySicCode { get; set; }
        public string? Region { get; set; }
        public string? RsvBankCode { get; set; }
        public string? ReturnsClassCode { get; set; }
        public string? ReturnsClassificationCode { get; set; }
        public string? SecondaryRmId { get; set; }
        public string? SecondarySicCode { get; set; }
        public string? ShortName { get; set; }
        public string? ValidateFnr { get; set; }
        public string? WithHoldingTax { get; set; }
        public string? NationalIdNumber { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? BaselIiIndicator { get; set; }
        public string? InsiderToBank { get; set; }
        public string? KycIndicator { get; set; }
        public string? PortfolioNumber { get; set; }
        public string? DistributionChannel { get; set; }
        public string? OnlyCountryOfTaxResidence { get; set; }
        public string? TaxIdNumber { get; set; }
        public string? TertiarySicCode { get; set; }
        public string? Base2Indicator { get; set; }
        public string? BranchId { get; set; }
        public string? CounterPartyInformation { get; set; }
        public string? DefaultAddressType { get; set; }
        public string? IdentityNumber { get; set; }
        public string? IndustryClassificationCode { get; set; }
        public string? IsCoreProfileActive { get; set; }
        public string? IsCustomerMinor { get; set; }
        public string? IsOnlyCountryOfNationality { get; set; }
        public string? IsOnlyCountryTaxResidence { get; set; }
        public string? KeyContactPersonName { get; set; }
        public string? PrimaryTaxResidenceCountry { get; set; }
        public string? Relationship { get; set; }
        public string? ReserveBankCode { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? WithholdingTax1 { get; set; }
        public string? Zip { get; set; }

        public virtual ICollection<RbxBpmNextOfKinDetail> RbxBpmNextOfKinDetails { get; set; }
    }
}
