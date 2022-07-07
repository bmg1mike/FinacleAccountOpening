using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class DownloadFileResponse
{
    public MemoryStream Memory { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }
}
