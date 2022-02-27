using System;
using System.ComponentModel.DataAnnotations;

namespace covid19_patients_tracker.Models.Dtos
{
    public class NewPatientRequest
    {
        public string GovtID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public int HouseResidentsAmount { get; set; }
        public bool IsCovidPositive { get; set; }
    }
}
