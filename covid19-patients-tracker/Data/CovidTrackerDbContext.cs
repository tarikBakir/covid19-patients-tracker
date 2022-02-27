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
        public DbSet<PatientEncounter> PatientEncounters { get; set; }
        public DbSet<LabTest> LabTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Person>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Patient>().HasKey(x => new { x.PatientID });
            modelBuilder.Entity<PotentialPatient>().HasKey(x => new { x.PotentialPatientID });
            modelBuilder.Entity<Address>().HasKey(x => new { x.Id });
            modelBuilder.Entity<LabTest>().HasKey(x => new { x.TestID });

            modelBuilder.Entity<PatientEncounter>()
                .HasKey(x => new { x.potentialPatientId, x.encounteredPatientId });


            modelBuilder.Entity<PatientEncounter>()
                .HasOne(x => x.potentialPatientDetails)
                .WithMany(u => u.PatientEcounters)
                .HasForeignKey(x => x.potentialPatientId);

            modelBuilder.Entity<PatientEncounter>()
                .HasOne(t => t.encounteredPatient)
                .WithMany(x => x.PatientEcounters)
                .HasForeignKey(x => x.encounteredPatientId);
        }
    }
}
