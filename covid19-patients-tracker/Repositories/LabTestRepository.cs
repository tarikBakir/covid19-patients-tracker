using covid19_patients_tracker.Data;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
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
        public async Task<LabTest> CreateLabTest(LabTest labTest)
        {
            _covidTrackerDbContext.LabTests.Add(labTest);
            await _covidTrackerDbContext.SaveChangesAsync();
            return labTest;
        }
    }
}
