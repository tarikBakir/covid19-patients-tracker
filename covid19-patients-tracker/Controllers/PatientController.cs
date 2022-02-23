using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Controllers
{
    [ApiController]
    [Route("api")]
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
    }
}
