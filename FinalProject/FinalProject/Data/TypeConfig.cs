using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    internal class TypeConfig : IEntityTypeConfiguration<ReportType>
    {
        public void Configure(EntityTypeBuilder<ReportType> entity)
        {
            entity.HasData(
                new ReportType { ReportTypeID = 1, Name = "WASA" },
                new ReportType { ReportTypeID = 2, Name = "WAPI" },
                new ReportType { ReportTypeID = 3, Name = "EPT"  },
                new ReportType { ReportTypeID = 4, Name = "IPT"  },
                new ReportType { ReportTypeID = 5, Name = "MASA" },
                new ReportType { ReportTypeID = 6, Name = "CASA" },
                new ReportType { ReportTypeID = 7, Name = "RedTeam"},
                new ReportType { ReportTypeID = 8, Name = "Phishing"},
                new ReportType { ReportTypeID = 9, Name = "VA"},
                new ReportType { ReportTypeID = 10, Name = "Other"}
                );
        }
    }
}
