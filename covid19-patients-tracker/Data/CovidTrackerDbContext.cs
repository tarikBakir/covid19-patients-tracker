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

        public DbSet<Person> People { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PotentialPatient> PotentialPatients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People").ToView("PeopleView");
            modelBuilder.Entity<Patient>().ToTable("Patients").ToView("PatientsView");
            modelBuilder.Entity<PotentialPatient>().ToTable("PotentialPatients").ToView("PotentialPatientsView");
            modelBuilder.Entity<Address>().ToTable("Addresses").ToView("AddressesView");
        }
    }
}
