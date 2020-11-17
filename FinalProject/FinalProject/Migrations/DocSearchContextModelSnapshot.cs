﻿// <auto-generated />
using System;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinalProject.Migrations
{
    [DbContext(typeof(DocSearchContext))]
    partial class DocSearchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalProject.Models.Consultant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Consultants");
                });

            modelBuilder.Entity("FinalProject.Models.Report", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Headings")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReportTypeID")
                        .HasColumnType("int");

                    b.Property<string>("SearchIndex")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ReportTypeID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("FinalProject.Models.ReportType", b =>
                {
                    b.Property<int>("ReportTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportTypeID");

                    b.ToTable("ReportTypes");

                    b.HasData(
                        new
                        {
                            ReportTypeID = 1,
                            Name = "WASA"
                        },
                        new
                        {
                            ReportTypeID = 2,
                            Name = "WAPI"
                        },
                        new
                        {
                            ReportTypeID = 3,
                            Name = "EPT"
                        },
                        new
                        {
                            ReportTypeID = 4,
                            Name = "IPT"
                        },
                        new
                        {
                            ReportTypeID = 5,
                            Name = "MASA"
                        },
                        new
                        {
                            ReportTypeID = 6,
                            Name = "CASA"
                        },
                        new
                        {
                            ReportTypeID = 7,
                            Name = "RedTeam"
                        },
                        new
                        {
                            ReportTypeID = 8,
                            Name = "Phishing"
                        },
                        new
                        {
                            ReportTypeID = 9,
                            Name = "VA"
                        },
                        new
                        {
                            ReportTypeID = 10,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("FinalProject.Models.Report", b =>
                {
                    b.HasOne("FinalProject.Models.ReportType", "ReportType")
                        .WithMany("Reports")
                        .HasForeignKey("ReportTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
