using covid19_patients_tracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Data
{
    public class CovidTrackerDbContext : DbContext
    {
        public CovidTrackerDbContext(DbContextOptions<CovidTrackerDbContext> options): base(options)
        {
        }

        //public DbSet<Person> People { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PotentialPatient> PotentialPatients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<LabTest> LabTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Person>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Patient>();
            modelBuilder.Entity<PotentialPatient>();
            modelBuilder.Entity<Address>().HasKey(x => new { x.Id });
            modelBuilder.Entity<LabTest>();
        }
    }
}
