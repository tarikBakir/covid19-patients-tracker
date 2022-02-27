using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Interfaces
{
    public interface ILabTestRepository
    {
        Task<LabTest> CreateLabTest(LabTestRequest labTest);
    }
}
