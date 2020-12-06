using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data.Repositories
{
    public interface IDocSearchUnitOfWork
    {
        Repository<Report> Reports { get; }
        Repository<ReportType> ReportTypes { get; }

        void Save();
    }
}
