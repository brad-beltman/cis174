using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module8.Models;

namespace AssignmentsApp.Areas.Module8.Data
{
    public class Module8Context : DbContext
    {
        public Module8Context(DbContextOptions<Module8Context> options)
            : base(options)
        { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusID = "todo", Name = "To Do" },
                new Status { StatusID = "inprogress", Name = "In Progress" },
                new Status { StatusID = "qa", Name = "Quality Assurance" },
                new Status { StatusID = "done", Name = "Done" });
        }
    }
}
