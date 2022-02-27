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
        [HttpGet]
        public async Task<IActionResult> GetListOfPotentialNotInserted()
        {
            // var result = await _patientRepository.GetListOfPotentialNotInserted();
            return Ok("");
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
        public async Task<IActionResult> TransferFromPotentialPatientToRealPatient([FromRoute] string potentialPatientId)
        {
            // var result = await _labTestRepository.TransferFromPotentialPatientToRealPatient(potentialPatientId);
            return Ok("");
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
