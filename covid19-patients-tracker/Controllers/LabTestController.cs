using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Controllers
{
    [ApiController]
    [Route("")]
    public class LabTestController : ControllerBase
    {
            private readonly ILabTestRepository _labTestRepository;

            [Route("labtests")]
            [HttpPost]
            public async Task<IActionResult> CreatePatient([FromBody] LabTestRequest labTest)
            {
                var result = await _labTestRepository.CreateLabTest(labTest);
                return Ok(result);
            }
    }
}
