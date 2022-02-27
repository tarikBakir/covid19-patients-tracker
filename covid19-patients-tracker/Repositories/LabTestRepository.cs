using covid19_patients_tracker.Data;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Repositories
{
    public class LabTestRepository : ILabTestRepository
    {
        private readonly CovidTrackerDbContext _covidTrackerDbContext;
        public LabTestRepository(CovidTrackerDbContext covidTrackerDbContext)
        {
            _covidTrackerDbContext = covidTrackerDbContext;
        }
        public async Task<string> CreateLabTest(LabTestRequest labTest)
        {
            var patient = await _covidTrackerDbContext.Patients.FindAsync(labTest.PatientID);
            var _labTest = new LabTest
            {
                LabID = labTest.LabID,
                TestID = labTest.TestID,
                Patient = patient,
                TestDate = labTest.TestDate,
                isCovidPositive = labTest.isCovidPositive,

            };
            _covidTrackerDbContext.LabTests.Add(_labTest);
            await _covidTrackerDbContext.SaveChangesAsync();

            return labTest.PatientID;
        }
    }
}
