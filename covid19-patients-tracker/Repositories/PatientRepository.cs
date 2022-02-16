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
            //_covidTrackerDbContext.Entry(patient.Address).State = EntityState.Detached;
            await _covidTrackerDbContext.SaveChangesAsync();
            return patient;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _covidTrackerDbContext.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _covidTrackerDbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
        }
    }
}
