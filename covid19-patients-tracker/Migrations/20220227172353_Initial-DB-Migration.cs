using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace covid19_patients_tracker.Migrations
{
    public partial class InitialDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PotentialPatients",
                columns: table => new
                {
                    PotentialPatientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotentialPatients", x => x.PotentialPatientID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GovId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    HouseMembersNumber = table.Column<int>(type: "int", nullable: false),
                    isCovidPositive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_Patients_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    TestID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LabID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isCovidPositive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_LabTests_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientEncounters",
                columns: table => new
                {
                    potentialPatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    encounteredPatientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientEncounters", x => new { x.potentialPatientId, x.encounteredPatientId });
                    table.ForeignKey(
                        name: "FK_PatientEncounters_Patients_encounteredPatientId",
                        column: x => x.encounteredPatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientEncounters_PotentialPatients_potentialPatientId",
                        column: x => x.potentialPatientId,
                        principalTable: "PotentialPatients",
                        principalColumn: "PotentialPatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_PatientID",
                table: "LabTests",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_encounteredPatientId",
                table: "PatientEncounters",
                column: "encounteredPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressId",
                table: "Patients",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "PatientEncounters");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "PotentialPatients");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
