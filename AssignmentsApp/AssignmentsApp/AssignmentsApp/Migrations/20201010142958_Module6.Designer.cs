﻿// <auto-generated />
using AssignmentsApp.Areas.Module6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssignmentsApp.Migrations
{
    [DbContext(typeof(Module6Context))]
    [Migration("20201010142958_Module6")]
    partial class Module6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssignmentsApp.Areas.Module6.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Game")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sport")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryID = 1,
                            Abbr = "CA",
                            Category = "Indoor",
                            Game = "Winter Olympics",
                            Name = "Canada",
                            Sport = "Curling"
                        },
                        new
                        {
                            CountryID = 2,
                            Abbr = "SE",
                            Category = "Indoor",
                            Game = "Winter Olympics",
                            Name = "Sweden",
                            Sport = "Curling"
                        },
                        new
                        {
                            CountryID = 3,
                            Abbr = "GB",
                            Category = "Indoor",
                            Game = "Winter Olympics",
                            Name = "Great Britain",
                            Sport = "Curling"
                        },
                        new
                        {
                            CountryID = 4,
                            Abbr = "JM",
                            Category = "Outdoor",
                            Game = "Winter Olympics",
                            Name = "Jamaica",
                            Sport = "Bobsleigh"
                        },
                        new
                        {
                            CountryID = 5,
                            Abbr = "IT",
                            Category = "Outdoor",
                            Game = "Winter Olympics",
                            Name = "Italy",
                            Sport = "Bobsleigh"
                        },
                        new
                        {
                            CountryID = 6,
                            Abbr = "JP",
                            Category = "Outdoor",
                            Game = "Winter Olympics",
                            Name = "Japan",
                            Sport = "Bobsleigh"
                        },
                        new
                        {
                            CountryID = 7,
                            Abbr = "DE",
                            Category = "Indoor",
                            Game = "Summer Olympics",
                            Name = "Germany",
                            Sport = "Diving"
                        },
                        new
                        {
                            CountryID = 8,
                            Abbr = "CN",
                            Category = "Indoor",
                            Game = "Summer Olympics",
                            Name = "China",
                            Sport = "Diving"
                        },
                        new
                        {
                            CountryID = 9,
                            Abbr = "MX",
                            Category = "Indoor",
                            Game = "Summer Olympics",
                            Name = "Mexico",
                            Sport = "Diving"
                        },
                        new
                        {
                            CountryID = 10,
                            Abbr = "BR",
                            Category = "Outdoor",
                            Game = "Summer Olympics",
                            Name = "Brazil",
                            Sport = "Road Cycling"
                        },
                        new
                        {
                            CountryID = 11,
                            Abbr = "NL",
                            Category = "Outdoor",
                            Game = "Summer Olympics",
                            Name = "Netherlands",
                            Sport = "Road Cycling"
                        },
                        new
                        {
                            CountryID = 12,
                            Abbr = "US",
                            Category = "Outdoor",
                            Game = "Summer Olympics",
                            Name = "United States",
                            Sport = "Road Cycling"
                        },
                        new
                        {
                            CountryID = 13,
                            Abbr = "TH",
                            Category = "Indoor",
                            Game = "Paralympics",
                            Name = "Thailand",
                            Sport = "Archery"
                        },
                        new
                        {
                            CountryID = 14,
                            Abbr = "UY",
                            Category = "Indoor",
                            Game = "Paralympics",
                            Name = "Uruguary",
                            Sport = "Archery"
                        },
                        new
                        {
                            CountryID = 15,
                            Abbr = "UA",
                            Category = "Indoor",
                            Game = "Paralympics",
                            Name = "Ukraine",
                            Sport = "Archery"
                        },
                        new
                        {
                            CountryID = 16,
                            Abbr = "AT",
                            Category = "Outdoor",
                            Game = "Paralympics",
                            Name = "Austria",
                            Sport = "Canoe Sprint"
                        },
                        new
                        {
                            CountryID = 17,
                            Abbr = "PK",
                            Category = "Outdoor",
                            Game = "Paralympics",
                            Name = "Pakistan",
                            Sport = "Canoe Sprint"
                        },
                        new
                        {
                            CountryID = 18,
                            Abbr = "ZW",
                            Category = "Outdoor",
                            Game = "Paralympics",
                            Name = "Zimbabwe",
                            Sport = "Canoe Sprint"
                        },
                        new
                        {
                            CountryID = 19,
                            Abbr = "FR",
                            Category = "Indoor",
                            Game = "Youth Olympic Games",
                            Name = "France",
                            Sport = "Breakdancing"
                        },
                        new
                        {
                            CountryID = 20,
                            Abbr = "CY",
                            Category = "Indoor",
                            Game = "Youth Olympic Games",
                            Name = "Cyprus",
                            Sport = "Breakdancing"
                        },
                        new
                        {
                            CountryID = 21,
                            Abbr = "RU",
                            Category = "Indoor",
                            Game = "Youth Olympic Games",
                            Name = "Russia",
                            Sport = "Breakdancing"
                        },
                        new
                        {
                            CountryID = 22,
                            Abbr = "FI",
                            Category = "Outdoor",
                            Game = "Youth Olympic Games",
                            Name = "Finland",
                            Sport = "Skateboarding"
                        },
                        new
                        {
                            CountryID = 23,
                            Abbr = "SK",
                            Category = "Outdoor",
                            Game = "Youth Olympic Games",
                            Name = "Slovakia",
                            Sport = "Skateboarding"
                        },
                        new
                        {
                            CountryID = 24,
                            Abbr = "PT",
                            Category = "Outdoor",
                            Game = "Youth Olympic Games",
                            Name = "Portugal",
                            Sport = "Skateboarding"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
