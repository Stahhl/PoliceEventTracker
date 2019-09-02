﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoliceEventTracker.Data.Models;

namespace PoliceEventTracker.Data.Migrations
{
    [DbContext(typeof(PoliceEventDbContext))]
    partial class PoliceEventDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PoliceEventTracker.Domain.Models.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Coordinate");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("EventId");

                    b.Property<long?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<string>("Summary");

                    b.Property<string>("Type");

                    b.Property<int?>("UpdateId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UpdateId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("PoliceEventTracker.Domain.Models.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("PoliceEventTracker.Domain.Models.Update", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<DateTime>("DateTime");

                    b.HasKey("Id");

                    b.ToTable("Updates");
                });

            modelBuilder.Entity("PoliceEventTracker.Domain.Models.Event", b =>
                {
                    b.HasOne("PoliceEventTracker.Domain.Models.Location", "Location")
                        .WithMany("Events")
                        .HasForeignKey("LocationId");

                    b.HasOne("PoliceEventTracker.Domain.Models.Update")
                        .WithMany("Events")
                        .HasForeignKey("UpdateId");
                });
#pragma warning restore 612, 618
        }
    }
}
