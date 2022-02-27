using covid19_patients_tracker.Data;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly CovidTrackerDbContext _covidTrackerDbContext;
        public PatientRepository(CovidTrackerDbContext covidTrackerDbContext)
        {
            _covidTrackerDbContext = covidTrackerDbContext;
        }
        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            _covidTrackerDbContext.Patients.Add(patient);
            await _covidTrackerDbContext.SaveChangesAsync();
            return patient;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _covidTrackerDbContext.Patients.Include(p => p.Address).ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(string id)
        {
            return await _covidTrackerDbContext.Patients.FirstOrDefaultAsync(patient => patient.PatientID == id);
        }

        public async Task<Patient> AddPatientEncounter(Patient patient, PotentialPatient potentialPatient)
        {
            await _covidTrackerDbContext.PotentialPatients.AddAsync(potentialPatient);

            PatientEncounter newpatientEncounter = new PatientEncounter
            {
                encounteredPatient = patient,
                potentialPatientDetails = potentialPatient
            };

            await _covidTrackerDbContext.PatientEncounters.AddAsync(newpatientEncounter);
            await _covidTrackerDbContext.SaveChangesAsync();

            return patient;
        }

        public async Task<List<PatientEncounter>> GetPatientEncounters(string patientId)
        {
            var result = await _covidTrackerDbContext.PatientEncounters.Where(p => p.encounteredPatientId == patientId).Include(enc => enc.encounteredPatient.Address).Include(pot => pot.potentialPatientDetails).ToListAsync();
            return result;
        }

        public async Task<List<SiteVisit>> GetPatientVisits(string patientId)
        {
            var result = await _covidTrackerDbContext.SiteVisits.Where(p => p.PatientId == patientId).Include(v => v.SiteAddress).ToListAsync();
            return result;
        }

        public async Task<SiteVisit> AddNewPatientVisit(string patientId, SiteVisit siteVisit)
        {
            await _covidTrackerDbContext.SiteVisits.AddAsync(siteVisit);
            await _covidTrackerDbContext.SaveChangesAsync();
            return siteVisit;
        }

        public async Task<PatientMedicalFile> GetPatientFullDetails(string id)
        {
            Patient patient =  await _covidTrackerDbContext.Patients.Where(p => p.PatientID == id).Include(p => p.Address).FirstOrDefaultAsync();
            List<LabTest> labTests = await _covidTrackerDbContext.LabTests.Where(l => l.Patient.PatientID == id).ToListAsync();

            GetPatientResponse patientDetails = new GetPatientResponse
            {
                PatientID = patient.PatientID,
                GovtID = patient.GovtId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.DateOfBirth,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                Address = patient.Address,
                HouseResidentsAmount = patient.HouseMembersNumber,
                infectedByPatientID = ""
            };

            List<LabTestRequest> labresults = new List<LabTestRequest>();

            foreach (var labTest in labTests)
            {
                labresults.Add(new LabTestRequest
                {
                    LabID = labTest.LabID,
                    TestDate = labTest.TestDate,
                    TestID = labTest.TestID,
                    PatientID = id,
                    isCovidPositive = labTest.isCovidPositive
                });
            }

            return new PatientMedicalFile
            {
                PatientDetails = patientDetails,
                IsCovidPositive = patient.isCovidPositive,
                LabResults = labresults
            };
        }
    }
}
