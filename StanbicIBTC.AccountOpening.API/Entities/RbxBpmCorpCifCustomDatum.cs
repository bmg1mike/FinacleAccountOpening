using System;
using System.Collections.Generic;

namespace StanbicIBTC.AccountOpening.API.Entities
{
    public partial class RbxBpmCorpCifCustomDatum
    {
        public RbxBpmCorpCifCustomDatum()
        {
            RbxBpmCorpCifCompanies = new HashSet<RbxBpmCorpCifCompany>();
        }

        public long Id { get; set; }
        public string? BusinessType { get; set; }
        public string? CorporateType { get; set; }
        public string? CounterPartyInfo { get; set; }
        public string? CountryOfTaxResidence { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerType { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? DistributionChannel { get; set; }
        public string? Earns50PercentGross { get; set; }
        public string? EntityClass { get; set; }
        public string? FatCaExempt { get; set; }
        public string? FinancialInstitution { get; set; }
        public string? FnrClassification { get; set; }
        public string? FnrStatus { get; set; }
        public string? FundSource { get; set; }
        public string? Holds50PercentGross { get; set; }
        public string? IdIssuedOrganization { get; set; }
        public string? IncomeTaxNumber { get; set; }
        public string? IndustryClassificationCode { get; set; }
        public string? InsiderToBank { get; set; }
        public string? KeyContactPerson { get; set; }
        public string? KycIndicator { get; set; }
        public string? LegalEntityType { get; set; }
        public string? MonthlyNetIncome { get; set; }
        public string? OnlyCountryOfTaxResidence { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PoliticallyExposed { get; set; }
        public string? PortfolioNumber { get; set; }
        public string? PreferredContactType { get; set; }
        public string? PrimaryEmailType { get; set; }
        public string? PrimarySicCode { get; set; }
        public string? Relationship { get; set; }
        public string? RsvBankCode { get; set; }
        public string? ReturnsClassCode { get; set; }
        public string? SbgMember { get; set; }
        public string? ScumRegNumber { get; set; }
        public string? SecondaryRmId { get; set; }
        public string? SecSicCode { get; set; }
        public string? StateOfInc { get; set; }
        public string? UniqueId { get; set; }
        public string? UsRelatedParties { get; set; }
        public string? W8BenReceived { get; set; }
        public string? Website { get; set; }
        public string? WithholdingTax { get; set; }
        public string? Bvn { get; set; }
        public string? LegalChallengeStatus { get; set; }
        public string? ListApprovedExchanges { get; set; }
        public string? Region { get; set; }
        public string? TertiarySicCode { get; set; }

        public virtual ICollection<RbxBpmCorpCifCompany> RbxBpmCorpCifCompanies { get; set; }
    }
}
