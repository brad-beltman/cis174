using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class DocSearchContext : DbContext
    {
        public DocSearchContext(DbContextOptions<DocSearchContext> options)
            : base(options)
        { }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TypeConfig());
        }
    }
}
