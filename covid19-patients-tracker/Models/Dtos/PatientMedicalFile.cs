

using System.Collections.Generic;

namespace covid19_patients_tracker.Models.Dtos
{
    public class PatientMedicalFile
    {
        public GetPatientResponse PatientDetails { get; set; }
        public bool IsCovidPositive { get; set; }

        public List<LabTestRequest> LabResults { get; set; }
    }
}
