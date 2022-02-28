using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace covid19_patients_tracker.Models
{
    public class LabTest
    {
        public string LabID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TestID { get; set; }
        public Patient Patient { get; set; }
        public DateTime TestDate { get; set; }
        public bool isCovidPositive { get; set; }
    }
}
