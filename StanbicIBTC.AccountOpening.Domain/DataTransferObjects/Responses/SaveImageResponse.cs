using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public  class SaveImageResponse
{
    public OutCome OutCome { get; set; }
    public string Udi { get; set; }
}
public class OutCome
{
    public string Status { get; set; }
}


