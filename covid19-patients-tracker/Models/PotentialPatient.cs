using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Models
{
    public class PotentialPatient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PotentialPatientID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [MaxLength(10)]
        [Required]
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public List<PatientEncounter> PatientEcounters { get; set; }
    }
}
