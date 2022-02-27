namespace covid19_patients_tracker.Models.Dtos
{
    public class PatientEncounterResponse
    {
        public GetPotentialPatientResponse PotentialPatientDetails { get; set; }
        public GetPatientResponse encounteredPatient { get; set; }
    }
}
