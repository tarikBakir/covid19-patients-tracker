using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace covid19_patients_tracker.Models
{
    public class SiteVisit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SiteVisitId { get; set; }
        public string PatientId { get; set; }

        public DateTime DateOfVisit { get; set; }

        public string SiteName { get; set; }

        public Address SiteAddress { get; set; }
    }
}
