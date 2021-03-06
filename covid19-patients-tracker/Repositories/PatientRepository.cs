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

        public async Task<PotentialPatient> GetPotentialPatientByIdAsync(string id)
        {
            return await _covidTrackerDbContext.PotentialPatients.FirstOrDefaultAsync(patient => patient.PotentialPatientID == id);
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
            Patient patient = await _covidTrackerDbContext.Patients.Where(p => p.PatientID == id).Include(p => p.Address).FirstOrDefaultAsync();
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

        public async Task<List<PatientEncounter>> GetAllPatientEncounters()
        {
            return await _covidTrackerDbContext.PatientEncounters.Include(p => p.encounteredPatient.Address).Include(p => p.potentialPatientDetails).ToListAsync();
        }

        public async Task<Patient> TransferFromPotentialPatientToRealPatient(string potentialPatientId, Patient newPatient)
        {
            PotentialPatient potentialPatientFound = await _covidTrackerDbContext.PotentialPatients.FirstOrDefaultAsync(p => p.PotentialPatientID == potentialPatientId);

            _covidTrackerDbContext.Remove(potentialPatientFound);

            await _covidTrackerDbContext.Patients.AddAsync(newPatient);

            await _covidTrackerDbContext.SaveChangesAsync();

            return newPatient;
        }

        public async Task<List<PatientEncounter>> GetListOfPatientsSince(DateTime since)
        {
            List<PatientEncounter> patientEncounters = await _covidTrackerDbContext.PatientEncounters.Include(p => p.encounteredPatient.Address).Include(p => p.potentialPatientDetails).ToListAsync();

            List<PatientEncounter> finalEncounters = new List<PatientEncounter>();

            foreach (PatientEncounter patientEncounter in patientEncounters)
            {
                if (patientEncounter.encounteredPatient.isCovidPositive && patientEncounter.encounteredPatient.CreatedOn > since)
                {
                    finalEncounters.Add(patientEncounter);
                }
            }

            return finalEncounters;
        }

        public async Task<List<PatientEncounter>> GetListOfIsolatedPeople()
        {
            List<Patient> patients = await _covidTrackerDbContext.Patients.ToListAsync();
            List<LabTest> labTests = await _covidTrackerDbContext.LabTests.Include(l => l.Patient).ToListAsync();
            List<string> isolatedPatintIds = new List<string>();

            foreach (Patient patient in patients)
            {
                int negativeTests = 0;
                int positiveTests = 0;
                foreach (LabTest labTest in labTests)
                {
                    if (labTest.Patient.PatientID == patient.PatientID)
                    {
                        if (labTest.isCovidPositive)
                        {
                            positiveTests++;
                        } else
                        {
                            negativeTests++;
                        }
                    }
                }
                if (negativeTests < 2)
                {
                    isolatedPatintIds.Add(patient.PatientID);
                }
            }

            List<PatientEncounter> patientEncounters = await _covidTrackerDbContext.PatientEncounters.Where(p => isolatedPatintIds.Contains(p.encounteredPatient.PatientID)).Include(p => p.encounteredPatient.Address).Include(p => p.potentialPatientDetails).ToListAsync();

            return patientEncounters;
        }

        public async Task<CovidStatistics> GetStatistics()
        {
            List<Patient> patients = await _covidTrackerDbContext.Patients.Include(x => x.Address).ToListAsync();

            var patientsGroupedByCity = patients.GroupBy(x => x.Address.City);

            List<LabTest> labTests = await _covidTrackerDbContext.LabTests.Include(l => l.Patient).ToListAsync();
            List<Patient> isolatedPatints = new List<Patient>();
            List<Patient> healedPatients = new List<Patient>();
            List<CityCovidStats> cityStatistics = new List<CityCovidStats>();

            int infected = 0;
            foreach (Patient patient in patients)
            {
                int negativeTests = 0;
                int positiveTests = 0;

                foreach (LabTest labTest in labTests)
                {
                    if (labTest.Patient.PatientID == patient.PatientID)
                    {
                        if (labTest.isCovidPositive)
                        {
                            positiveTests++; 
                        }
                        else
                        {
                            negativeTests++;
                        }
                    }
                }
                if (negativeTests < 2)
                {
                    isolatedPatints.Add(patient);
                    infected++;
                } else
                {
                    healedPatients.Add(patient);
                }

            }
            foreach (var patient in patientsGroupedByCity)
            {
                var cityStat = new CityCovidStats()
                {
                    Infected = patient.ToList().Count,
                    City = patient.Key
                };
                cityStatistics.Add(cityStat);
            }

            var statistics = new CovidStatistics()
            {
                Infected = patients.Count,
                Isolated = isolatedPatints.Count,
                Healed = healedPatients.Count,
                CityStatistics = cityStatistics
            };

            return statistics;
        }
    }
}
