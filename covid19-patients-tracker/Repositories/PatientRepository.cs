using covid19_patients_tracker.Data;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
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
            //_covidTrackerDbContext.Entry(FoundCategory).Collection(x => x.BlogCategories).Load();
            return result;
        }
    }
}
