using System;

namespace covid19_patients_tracker.Models.DTOs
{
    public class LabTestRequest
    {
        public string LabID { get; set; }
        public string TestID { get; set; }
        public string PatientID { get; set; }
        public DateTime TestDate { get; set; }
        public bool isCovidPositive { get; set; }
    }
}
