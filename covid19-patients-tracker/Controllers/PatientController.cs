using covid19_patients_tracker.Dtos;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Controllers
{
    [ApiController]
    [Route("")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;


        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        [Route("patients")]
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await _patientRepository.GetAllPatientsAsync();
            return Ok(result);
        }

        [Route("patients")]
        [HttpPut]
        public async Task<IActionResult> CreatePatient([FromBody] NewPatientRequest newPatintRequest)
        {
            Patient newPatient = new Patient {
                GovtId = newPatintRequest.GovtID,
                FirstName = newPatintRequest.FirstName,
                LastName = newPatintRequest.LastName,
                DateOfBirth = newPatintRequest.BirthDate,
                PhoneNumber = newPatintRequest.PhoneNumber,
                Email = newPatintRequest.Email,
                Address = newPatintRequest.Address,
                HouseMembersNumber = newPatintRequest.HouseResidentsAmount,
                isCovidPositive = newPatintRequest.IsCovidPositive
            };
            var result = await _patientRepository.CreatePatientAsync(newPatient);
            return Ok(new { patientID = result.PatientID });
        }

        [Route("patients/{id}/encounters")]
        [HttpPut]
        public async Task<IActionResult> AddPatientEncounter([FromRoute] string id, [FromBody] NewPatientEncounter newPatintEncounter)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Patient ID not provided." });
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = "Patient Not Found." }); ;
            }

            PotentialPatient potentialPatient = new PotentialPatient {
                FirstName = newPatintEncounter.FirstName,
                LastName = newPatintEncounter.LastName,
                PhoneNumber = newPatintEncounter.PhoneNumber
            };
            
            var potential = await _patientRepository.AddPatientEncounter(patient, potentialPatient);
            return Ok(new { });
        }

        [Route("patients/{id}/encounters")]
        [ProducesResponseType(typeof(List<PatientEncounter>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetPatientEncounters([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Patient ID not provided." });
            }

            List<PatientEncounter> result = await _patientRepository.GetPatientEncounters(id);
            return Ok(result);
        }

        [Route("patients/{id}/route")]
        [HttpPut]
        public async Task<IActionResult> AddLocationVisit([FromRoute] string id, [FromBody] NewPatientVisitedSite newPatintSite)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Patient ID not provided." });
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = "Patient Not Found." }); ;
            }

            SiteVisit siteVisit = new SiteVisit
            {
                SiteName = newPatintSite.SiteName,
                SiteAddress = newPatintSite.SiteAddress,
                DateOfVisit = newPatintSite.DateOfVisit,
                PatientId = id
            };

            var result = await _patientRepository.AddNewPatientVisit(id, siteVisit);
            return Ok(new { });
        }

        [Route("patients/{id}/route")]
        [ProducesResponseType(typeof(List<NewPatientVisitedSite>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetListLocations([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Patient ID not provided." });
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = "Patient Not Found." }); ;
            }

            List<SiteVisit> result = await _patientRepository.GetPatientVisits(id);
            List<NewPatientVisitedSite> sitesVisited = new List<NewPatientVisitedSite>();

            foreach(SiteVisit siteVisit in result)
            {
                sitesVisited.Add(new NewPatientVisitedSite {
                    SiteName = siteVisit.SiteName,
                    SiteAddress = siteVisit.SiteAddress,
                    DateOfVisit = siteVisit.DateOfVisit
                });
            }

            return Ok(sitesVisited);
        }

        [Route("patients/{id}/full")]
        [ProducesResponseType(typeof(PatientMedicalFile), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllDeatilsByPatientId([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { message = "Patient ID not provided." });
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = "Patient Not Found." }); ;
            }

            var result = await _patientRepository.GetPatientFullDetails(id);
            return Ok(result);
        }

        [Route("patients/new")]
        [HttpGet]
        public async Task<IActionResult> GetListOfPatientsSince([FromBody] DateTime date)
        {
            //if (string.IsNullOrWhiteSpace(date))
            //{
            //    throw new ArgumentNullException(nameof(date));
            //}

            // var result = await _patientRepository.GetListOfPatientsSince(date);
            return Ok("");
        }

        [Route("patients/potential")]
        [ProducesResponseType(typeof(List<PatientEncounterResponse>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetListOfPotentialNotInserted()
        {
            List<PatientEncounter> result = await _patientRepository.GetAllPatientEncounters();
            List<PatientEncounterResponse> patientEncounters = new List<PatientEncounterResponse>();

            foreach (PatientEncounter enc in result)
            {
                patientEncounters.Add(new PatientEncounterResponse { 
                    PotentialPatientDetails = new GetPotentialPatientResponse { 
                        PotentialPatientID = enc?.potentialPatientDetails?.PotentialPatientID,
                        FirstName = enc?.potentialPatientDetails?.FirstName,
                        LastName = enc?.potentialPatientDetails?.LastName,
                        PhoneNumber = enc?.potentialPatientDetails?.PhoneNumber
                    },
                    encounteredPatient = new GetPatientResponse { 
                        PatientID = enc?.encounteredPatient?.PatientID,
                        GovtID = enc?.encounteredPatient?.GovtId,
                        FirstName = enc?.encounteredPatient?.FirstName,
                        LastName= enc?.encounteredPatient?.LastName,
                        BirthDate = enc.encounteredPatient.DateOfBirth,
                        PhoneNumber = enc?.encounteredPatient?.PhoneNumber,
                        Email = enc?.encounteredPatient?.Email,
                        Address = enc?.encounteredPatient?.Address,
                        HouseResidentsAmount = enc.encounteredPatient.HouseMembersNumber,
                        infectedByPatientID = ""
                    }
                });
            }

            return Ok(patientEncounters);
        }

        [Route("patients/isolated")]
        [HttpGet]
        public async Task<IActionResult> GetListOfIsolatedPeople()
        {
            // var result = await _patientRepository.GetListOfIsolatedPeople();
            return Ok("");
        }

        [Route("patients/potential/{potentialPatientId}")]
        [HttpPost]
        public async Task<IActionResult> TransferFromPotentialPatientToRealPatient([FromRoute] string potentialPatientId, [FromBody] NewPatientRequest newPatient)
        {
            if (string.IsNullOrWhiteSpace(potentialPatientId))
            {
                return BadRequest(new { message = "Potential Patient ID not provided." });
            }

            var potentialPatient = await _patientRepository.GetPotentialPatientByIdAsync(potentialPatientId);
            if (potentialPatient == null)
            {
                return NotFound(new { Message = "Potential Patient Not Found." });
            }

            Patient patient = new Patient
            {
                GovtId = newPatient.GovtID,
                FirstName = newPatient.FirstName,
                LastName = newPatient.LastName,
                DateOfBirth = newPatient.BirthDate,
                PhoneNumber = newPatient.PhoneNumber,
                Email = newPatient.Email,
                Address = newPatient.Address,
                HouseMembersNumber = newPatient.HouseResidentsAmount,
                isCovidPositive = newPatient.IsCovidPositive
            };

            Patient result = await _patientRepository.TransferFromPotentialPatientToRealPatient(potentialPatientId, patient);

            return Ok(new { PatientID = result.PatientID });
        }

        [Route("statistics")]
        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            // var result = await _labTestRepository.GetStatistics();
            return Ok("");
        }
    }
}
