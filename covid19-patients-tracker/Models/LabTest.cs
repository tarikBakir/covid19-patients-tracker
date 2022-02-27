using System;

namespace covid19_patients_tracker.Models
{
    public class LabTest
    {
        public string LabID { get; set; }
        public string TestID { get; set; }
        public Patient PatientID { get; set; }
        public DateTime TestDate { get; set; }
        public bool isCovidPositive { get; set; }
    }
}
