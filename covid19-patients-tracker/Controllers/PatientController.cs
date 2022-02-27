﻿using covid19_patients_tracker.Interfaces;
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

        //[Route("{id}")]
        //public async Task<IActionResult> GetPatientById(int id)
        //{
        //    var result = await _patientRepository.GetPatientByIdAsync(id);
        //    return Ok(result);
        //}

        [Route("patients")]
        [HttpPut]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            var result = await _patientRepository.CreatePatientAsync(patient);
            return Ok(result);
        }

        [Route("patients/{id}/encounters")]
        [HttpPut]
        public async Task<IActionResult> AddPatientEncounter([FromRoute] string id, [FromBody] NewPatientEncounter newPatintEncounter)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            PotentialPatient potentialPatient = new PotentialPatient{
                FirstName = newPatintEncounter.FirstName,
                LastName = newPatintEncounter.LastName,
                PhoneNumber = newPatintEncounter.PhoneNumber
            };
            
            var potential = await _patientRepository.AddPatientEncounter(patient, potentialPatient);
            return Ok(potentialPatient);
        }

        [Route("patients/{id}/encounters")]
        [HttpGet]
        public async Task<IActionResult> GetPatientEncounters([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _patientRepository.GetPatientEncounters(id);
            return Ok(result);
        }

        [Route("patients/{id}/route")]
        [HttpPut]
        public async Task<IActionResult> AddLocationVisit([FromRoute] string id, [FromBody] NewPatientVisitedSite newPatintEncounter)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            // var result = await _patientRepository.AddLocationVisitAsync(int.Parse(id));
            return Ok(result);
        }

        [Route("patients/{id}/route")]
        [HttpGet]
        public async Task<IActionResult> GetListLocations([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            // var result = await _patientRepository.GetListLocationsAsync(id);
            return Ok(result);
        }

        [Route("patients/{id}/full")]
        [HttpGet]
        public async Task<IActionResult> GetAllDeatilsByPatientId([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            // var result = await _patientRepository.GetAllDeatilsByPatientId(id);
            return Ok(result);
        }

        [Route("patients/new")]
        [HttpGet]
        public async Task<IActionResult> GetListOfPatientsSince([FromBody] DateTime date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                throw new ArgumentNullException(nameof(date));
            }

            // var result = await _patientRepository.GetListOfPatientsSince(date);
            return Ok(result);
        }

        [Route("patients/potential")]
        [HttpGet]
        public async Task<IActionResult> GetListOfPotentialNotInserted()
        {
            // var result = await _patientRepository.GetListOfPotentialNotInserted();
            return Ok(result);
        }

        [Route("patients/isolated")]
        [HttpGet]
        public async Task<IActionResult> GetListOfIsolatedPeople()
        {
            // var result = await _patientRepository.GetListOfIsolatedPeople();
            return Ok(result);
        }

        [Route("patients/potential/{potentialPatientId}")]
        [HttpPost]
        public async Task<IActionResult> TransferFromPotentialPatientToRealPatient([FromRoute] string potentialPatientId)
        {
            // var result = await _labTestRepository.TransferFromPotentialPatientToRealPatient(potentialPatientId);
            return Ok(result);
        }

        [Route("statistics")]
        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            // var result = await _labTestRepository.GetStatistics();
            return Ok(result);
        }
    }
}
