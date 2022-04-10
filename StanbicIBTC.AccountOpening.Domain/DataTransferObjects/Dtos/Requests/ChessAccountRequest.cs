using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;
    public class ChessAccountRequest
    {
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }

        public string Bvn { get; set; }
        public string ChildBirthCertificate { get; set; }
        public DateTime ChildDateOfBirth { get; set; }
        public string Photograph { get; set; }
        public Platform Platform { get; set; }
    }