using covid19_patients_tracker.Controllers;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid19_patients_tracker_unit_test.Controllers
{
    [TestClass]
    public class PatientsControllerUnitTests
    {
        private Mock<IPatientRepository>? _patientRepository;
        private PatientController? _patientController;

        [TestInitialize]
        public void TestInit()
        {
            _patientRepository = new Mock<IPatientRepository>();

            List<Patient> mockPatients = new List<Patient>{
                new Patient {
                    PatientID = "1234565789",
                    FirstName = "test patient",
                    LastName =  "last name patient"
                },
                new Patient {
                    PatientID = "544345343434343",
                    FirstName = "test patient2",
                    LastName =  "last name patient2"
                }
            };

            Task<List<Patient>> mockPatientsTask = Task.FromResult(mockPatients);

            _patientRepository.Setup(p => p.GetAllPatientsAsync()).Returns(mockPatientsTask);

            _patientController = new PatientController(_patientRepository.Object);
        }

        [TestMethod]
        public async Task GetAllPatints_should_return_all_patients_in_the_system()
        {
            var result = await _patientController.GetAllPatients();

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Patient>), "Returned Object Type is not a type of Patient List");

            List<Patient>? patients = okResult.Value as List<Patient>;

            // check if there are right amount of patients returned
            Assert.AreEqual(2, patients.Count, "The length of returned patients list is incorrect.");
        }
    }
}
