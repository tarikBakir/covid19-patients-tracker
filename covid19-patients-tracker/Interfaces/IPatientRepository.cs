using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
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

        Task<SiteVisit> AddNewPatientVisit(string patientID, SiteVisit siteVisit);

        Task<List<SiteVisit>> GetPatientVisits(string patientID);

        Task<PatientMedicalFile> GetPatientFullDetails(string patientID);

        Task<List<PatientEncounter>> GetAllPatientEncounters();

        Task<Patient> TransferFromPotentialPatientToRealPatient(string potentialPatientId, Patient newPatient);

        Task<PotentialPatient> GetPotentialPatientByIdAsync(string id);

        Task<List<PatientEncounter>> GetListOfPatientsSince(DateTime since);
    }
}
