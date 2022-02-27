using Newtonsoft.Json;

namespace covid19_patients_tracker.Models
{
    public class PatientEncounter
    {
        public string potentialPatientId { get; set; }

        public string encounteredPatientId { get; set; }

        public PotentialPatient potentialPatientDetails { get; set; }

        public Patient encounteredPatient { get; set; }
    }
}
