using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Models
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PatientID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [MaxLength(10)]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string GovtId { get; set; }
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
        public DateTime CreatedOn { get; set; }

        [JsonIgnore]
        public List<PatientEncounter> PatientEcounters { get; set; }
    }
}
