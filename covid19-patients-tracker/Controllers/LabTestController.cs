using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Controllers
{
    [ApiController]
    [Route("")]
    public class LabTestController : ControllerBase
    {
            private readonly ILabTestRepository _labTestRepository;

            public LabTestController(ILabTestRepository labRepository)
            {
                _labTestRepository = labRepository;
            }

            [Route("labtests")]
            [HttpPost]
            public async Task<IActionResult> CreatePatientLabTest([FromBody] LabTestRequest labTest)
            {
                try
                {
                    var result = await _labTestRepository.CreateLabTest(labTest);
                    return Ok(new { PatintID = result.Patient.PatientID });

                } 
                catch (System.Exception er)
                {
                    return BadRequest(er.Message);
                }
            }
    }
}
