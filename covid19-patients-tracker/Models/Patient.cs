using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Models
{
    public class Patient : Person
    {
        [Required]
        public string GovId { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public int HouseMembersNumber { get; set; }
        [Required]
        public bool isCovidPositive { get; set; }

        [JsonIgnore]
        public List<PatientEncounter> PatientEcounters { get; set; }
    }
}
