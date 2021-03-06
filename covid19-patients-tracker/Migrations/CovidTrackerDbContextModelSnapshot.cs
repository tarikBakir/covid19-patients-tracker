// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using covid19_patients_tracker.Data;

namespace covid19_patients_tracker.Migrations
{
    [DbContext(typeof(CovidTrackerDbContext))]
    partial class CovidTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("covid19_patients_tracker.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.LabTest", b =>
                {
                    b.Property<string>("TestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LabID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("TestDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isCovidPositive")
                        .HasColumnType("bit");

                    b.HasKey("TestID");

                    b.HasIndex("PatientID");

                    b.ToTable("LabTests");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.Patient", b =>
                {
                    b.Property<string>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GovtId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseMembersNumber")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("isCovidPositive")
                        .HasColumnType("bit");

                    b.HasKey("PatientID");

                    b.HasIndex("AddressId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.PatientEncounter", b =>
                {
                    b.Property<string>("potentialPatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("encounteredPatientId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("potentialPatientId", "encounteredPatientId");

                    b.HasIndex("encounteredPatientId");

                    b.ToTable("PatientEncounters");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.PotentialPatient", b =>
                {
                    b.Property<string>("PotentialPatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("PotentialPatientID");

                    b.ToTable("PotentialPatients");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.SiteVisit", b =>
                {
                    b.Property<string>("SiteVisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfVisit")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SiteAddressId")
                        .HasColumnType("int");

                    b.Property<string>("SiteName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SiteVisitId");

                    b.HasIndex("SiteAddressId");

                    b.ToTable("SiteVisits");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.LabTest", b =>
                {
                    b.HasOne("covid19_patients_tracker.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.Patient", b =>
                {
                    b.HasOne("covid19_patients_tracker.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.PatientEncounter", b =>
                {
                    b.HasOne("covid19_patients_tracker.Models.Patient", "encounteredPatient")
                        .WithMany("PatientEcounters")
                        .HasForeignKey("encounteredPatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("covid19_patients_tracker.Models.PotentialPatient", "potentialPatientDetails")
                        .WithMany("PatientEcounters")
                        .HasForeignKey("potentialPatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("encounteredPatient");

                    b.Navigation("potentialPatientDetails");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.SiteVisit", b =>
                {
                    b.HasOne("covid19_patients_tracker.Models.Address", "SiteAddress")
                        .WithMany()
                        .HasForeignKey("SiteAddressId");

                    b.Navigation("SiteAddress");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.Patient", b =>
                {
                    b.Navigation("PatientEcounters");
                });

            modelBuilder.Entity("covid19_patients_tracker.Models.PotentialPatient", b =>
                {
                    b.Navigation("PatientEcounters");
                });
#pragma warning restore 612, 618
        }
    }
}
