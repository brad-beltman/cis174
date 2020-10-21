﻿// <auto-generated />
using AssignmentsApp.Areas.Module8.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssignmentsApp.Migrations.Module8
{
    [DbContext(typeof(Module8Context))]
    [Migration("20201020142341_Module8 Status Added")]
    partial class Module8StatusAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssignmentsApp.Areas.Module8.Models.Status", b =>
                {
                    b.Property<string>("StatusID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusID");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            StatusID = "ToDo",
                            Name = "To Do"
                        },
                        new
                        {
                            StatusID = "InProgress",
                            Name = "In Progress"
                        },
                        new
                        {
                            StatusID = "QA",
                            Name = "Quality Assurance"
                        },
                        new
                        {
                            StatusID = "Done",
                            Name = "Done"
                        });
                });

            modelBuilder.Entity("AssignmentsApp.Areas.Module8.Models.Ticket", b =>
                {
                    b.Property<int>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PointValue")
                        .HasColumnType("int");

                    b.Property<int>("SprintNumber")
                        .HasColumnType("int");

                    b.Property<string>("StatusID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TicketID");

                    b.HasIndex("StatusID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("AssignmentsApp.Areas.Module8.Models.Ticket", b =>
                {
                    b.HasOne("AssignmentsApp.Areas.Module8.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}