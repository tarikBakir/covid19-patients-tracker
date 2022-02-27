using System;

namespace covid19_patients_tracker.Models
{
    public class PatientMedicalFile
    {
        public string PatientID { get; set; }
        public string GovtID { get; set; }
        public string FirstName { get; set; }
        public string LatName { get; set; }
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public int HouseResidentAmount { get; set; }

        public string InfectedByPatientID { get; set; }

    }
}
