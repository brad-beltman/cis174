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

        string CreateSearchIndex(byte[] bytes);
    }
}
