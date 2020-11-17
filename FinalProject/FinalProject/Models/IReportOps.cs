using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProject.Models
{
    public interface IReportOps
    {
        XElement ConvertToHTML(byte[] bytes);

        Dictionary<string, string> CreateSearchIndex(byte[] bytes);
    }
}
