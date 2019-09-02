using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoliceEventTracker.Domain.Models;
using PoliceEventTracker.Domain.Other;

namespace PoliceEventTracker.Data.Models
{
    public class PoliceEventDbContext : DbContext
    {
        //If adding migration use hardcoded values
        //TODO fix this
        public PoliceEventDbContext()
        {
            applicationSettings = new ApplicationSettings(
                "Server = (localdb)\\mssqllocaldb; Database = PoliceEventDb; Trusted_Connection = True",
                "https://polisen.se/api/events"
                );
        }
        //If running the app get values from dependancy injection
        public PoliceEventDbContext(ApplicationSettings settings)
        {
            applicationSettings = settings;
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Update> Updates { get; set; }


        private ApplicationSettings applicationSettings;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(applicationSettings.DbConnection);
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
