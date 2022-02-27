namespace covid19_patients_tracker.Models.Dtos
{
    public class GetPotentialPatientResponse
    {
        public string PotentialPatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
