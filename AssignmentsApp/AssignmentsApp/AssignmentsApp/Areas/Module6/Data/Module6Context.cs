using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentsApp.Areas.Module6.Models;

namespace AssignmentsApp.Areas.Module6.Data
{
    public class Module6Context : DbContext
    {
        public Module6Context(DbContextOptions<Module6Context> options)
            :base(options)
        { }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    CountryID = 1,
                    Name = "Canada",
                    Abbr = "CA",
                    Game = "Winter Olympics",
                    Sport = "Curling",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 2,
                    Name = "Sweden",
                    Abbr = "SE",
                    Game = "Winter Olympics",
                    Sport = "Curling",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 3,
                    Name = "Great Britain",
                    Abbr = "GB",
                    Game = "Winter Olympics",
                    Sport = "Curling",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 4,
                    Name = "Jamaica",
                    Abbr = "JM",
                    Game = "Winter Olympics",
                    Sport = "Bobsleigh",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 5,
                    Name = "Italy",
                    Abbr = "IT",
                    Game = "Winter Olympics",
                    Sport = "Bobsleigh",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 6,
                    Name = "Japan",
                    Abbr = "JP",
                    Game = "Winter Olympics",
                    Sport = "Bobsleigh",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 7,
                    Name = "Germany",
                    Abbr = "DE",
                    Game = "Summer Olympics",
                    Sport = "Diving",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 8,
                    Name = "China",
                    Abbr = "CN",
                    Game = "Summer Olympics",
                    Sport = "Diving",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 9,
                    Name = "Mexico",
                    Abbr = "MX",
                    Game = "Summer Olympics",
                    Sport = "Diving",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 10,
                    Name = "Brazil",
                    Abbr = "BR",
                    Game = "Summer Olympics",
                    Sport = "Road Cycling",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 11,
                    Name = "Netherlands",
                    Abbr = "NL",
                    Game = "Summer Olympics",
                    Sport = "Road Cycling",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 12,
                    Name = "United States",
                    Abbr = "US",
                    Game = "Summer Olympics",
                    Sport = "Road Cycling",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 13,
                    Name = "Thailand",
                    Abbr = "TH",
                    Game = "Paralympics",
                    Sport = "Archery",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 14,
                    Name = "Uruguary",
                    Abbr = "UY",
                    Game = "Paralympics",
                    Sport = "Archery",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 15,
                    Name = "Ukraine",
                    Abbr = "UA",
                    Game = "Paralympics",
                    Sport = "Archery",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 16,
                    Name = "Austria",
                    Abbr = "AT",
                    Game = "Paralympics",
                    Sport = "Canoe Sprint",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 17,
                    Name = "Pakistan",
                    Abbr = "PK",
                    Game = "Paralympics",
                    Sport = "Canoe Sprint",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 18,
                    Name = "Zimbabwe",
                    Abbr = "ZW",
                    Game = "Paralympics",
                    Sport = "Canoe Sprint",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 19,
                    Name = "France",
                    Abbr = "FR",
                    Game = "Youth Olympic Games",
                    Sport = "Breakdancing",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 20,
                    Name = "Cyprus",
                    Abbr = "CY",
                    Game = "Youth Olympic Games",
                    Sport = "Breakdancing",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 21,
                    Name = "Russia",
                    Abbr = "RU",
                    Game = "Youth Olympic Games",
                    Sport = "Breakdancing",
                    Category = "Indoor"
                },

                new Country
                {
                    CountryID = 22,
                    Name = "Finland",
                    Abbr = "FI",
                    Game = "Youth Olympic Games",
                    Sport = "Skateboarding",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 23,
                    Name = "Slovakia",
                    Abbr = "SK",
                    Game = "Youth Olympic Games",
                    Sport = "Skateboarding",
                    Category = "Outdoor"
                },

                new Country
                {
                    CountryID = 24,
                    Name = "Portugal",
                    Abbr = "PT",
                    Game = "Youth Olympic Games",
                    Sport = "Skateboarding",
                    Category = "Outdoor"
                }
            );
        }
    }
}
