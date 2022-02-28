using System;

namespace covid19_patients_tracker.Models.Dtos
{
    public class GetPatientResponse
    {
        public string PatientID { get; set; }
        public string GovtID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public int HouseResidentsAmount { get; set; }
        public string infectedByPatientID { get; set; }
    }
}
