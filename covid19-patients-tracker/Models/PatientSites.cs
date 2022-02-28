using System;

namespace covid19_patients_tracker.Models
{
    public class PatientSites
    {
        public string PatientId { get; set; }

        public string SiteId { get; set; }

        public DateTime DateOfVisit { get; set; }
    }
}
