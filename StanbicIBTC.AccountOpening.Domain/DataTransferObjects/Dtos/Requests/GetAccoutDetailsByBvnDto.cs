using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class GetAccoutDetailsByBvnDto
{
    [Required]
    public string Bvn { get; set; }
}
