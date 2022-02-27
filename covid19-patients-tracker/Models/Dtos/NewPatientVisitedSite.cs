using covid19_patients_tracker.Models;
using System;

namespace covid19_patients_tracker.Dtos
{
    public class NewPatientVisitedSite
    {
        public DateTime DateOfVisit { get; set; }

        public string SiteName { get; set; }

        public Address SiteAddress { get; set; }
    }
}
