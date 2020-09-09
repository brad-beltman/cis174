using Microsoft.EntityFrameworkCore;
using MultiPageAppBeltman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiPageAppBeltman.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext (DbContextOptions<ContactContext> options)
            : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactID = 1,
                    Name = "Homer Simpson",
                    Phone = "555-555-5555",
                    Address = "642 Evergreen Terrace, Springfield, KY 12345",
                    Notes = "Met at Moes"
                },
                new Contact
                {
                    ContactID = 2,
                    Name = "Lenny Leonard",
                    Phone = "555-555-5555",
                    Address = "123 Fake St, Springfield, KY 12345",
                    Notes = "Stonecutter #12"
                },
                new Contact
                {
                    ContactID = 3,
                    Name = "Carl Carlson",
                    Phone = "555-555-5555",
                    Address = "987 Fake St, Springfield, KY 12345",
                    Notes = "Stonecutter #14"
                });
        }
    }
}
