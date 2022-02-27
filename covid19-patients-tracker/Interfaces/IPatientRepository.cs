using covid19_patients_tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(string id);
        Task<Patient> CreatePatientAsync(Patient patient);

        Task<Patient> AddPatientEncounter(Patient patient, PotentialPatient potentialPatient);

        Task<List<PatientEncounter>> GetPatientEncounters(string patientId);
    }
}
