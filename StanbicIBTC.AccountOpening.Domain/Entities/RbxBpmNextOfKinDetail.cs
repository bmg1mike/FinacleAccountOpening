using System;
using System.Collections.Generic;

namespace StanbicIBTC.AccountOpening.Domain
{
    public partial class RbxBpmNextOfKinDetail
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Address { get; set; }
        public string AddressFormat { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string RelationshipType { get; set; }
        public string FkCustomerAddDetails { get; set; }

        public  RbxBpmCifCustomDatum FkCustomerAddDetailsNavigation { get; set; }
    }
}
