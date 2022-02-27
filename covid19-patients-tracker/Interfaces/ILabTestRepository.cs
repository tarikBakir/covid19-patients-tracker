using covid19_patients_tracker.Models;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Interfaces
{
    public interface ILabTestRepository
    {
        Task<LabTest> CreateLabTest(LabTest labTest);
    }
}
