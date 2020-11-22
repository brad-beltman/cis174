using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data.Repositories
{
    public class DocSearchUnitOfWork : IDocSearchUnitOfWork
    {
        private DocSearchContext context { get; set; }
        public DocSearchUnitOfWork(DocSearchContext ctx) => context = ctx;

        private Repository<Report> reportData;
        public Repository<Report> Reports
        {
            get
            {
                if (reportData == null)
                {
                    reportData = new Repository<Report>(context);
                }
                return reportData;
            }
        }

        private Repository<ReportType> reportTypesData;
        public Repository<ReportType> ReportTypes
        {
            get
            {
                if (reportTypesData == null)
                {
                    reportTypesData = new Repository<ReportType>(context);
                }
                return reportTypesData;
            }
        }

        //private Repository<Consultant> consultantData;
        //public Repository<Consultant> Consultants
        //{
        //    get
        //    {
        //        if (consultantData == null)
        //        {
        //            consultantData = new Repository<Consultant>(context);
        //        }
        //        return consultantData;
        //    }
        //}

        public void Save() => context.SaveChanges();
    }
}
