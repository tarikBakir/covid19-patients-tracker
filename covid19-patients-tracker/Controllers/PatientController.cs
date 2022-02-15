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
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Patient
            {
                Id = 1,
                GovId = "207489738",
                PatientId = "1",
                Address = new Address
                {
                    City = "Tel Aviv",
                    ApartmentNumber = 0,
                    HouseNumber = 89,
                    Street = "Street Test Name"
                },
                DateOfBirth = new DateTime(),
                FirstName = "Jack",
                LastName = "Wiggle",
                Email = "Jack@test.com",
                HouseMembersNumber = 3,
                PhoneNumber = "0549552948"
            })
            .ToArray();
        }
    }
}
