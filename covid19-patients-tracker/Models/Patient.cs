using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Models
{
    public class Patient : Person
    {
        public string GovId { get; set; }
        public string PatientId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public int HouseMembersNumber { get; set; }
    }
}
