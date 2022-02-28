using covid19_patients_tracker.Controllers;
using covid19_patients_tracker.Dtos;
using covid19_patients_tracker.Interfaces;
using covid19_patients_tracker.Models;
using covid19_patients_tracker.Models.Dtos;
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

            List<PatientEncounter> patEncounters = new List<PatientEncounter>
            {
                new PatientEncounter { potentialPatientDetails = new PotentialPatient(), encounteredPatient = new Patient() } 
            };

            _patientRepository.Setup(p => p.GetAllPatientsAsync()).Returns(mockPatientsTask);
            _patientRepository.Setup(p => p.CreatePatientAsync(newCreatePatient)).Returns(Task.FromResult(newCreatePatient));
            _patientRepository.Setup(p => p.GetPatientVisits("sample_id")).Returns(Task.FromResult(new List<SiteVisit>()));
            _patientRepository.Setup(p => p.GetPatientEncounters("sample_id")).Returns(Task.FromResult(patEncounters));
            _patientRepository.Setup(p => p.GetPatientFullDetails("sample_id")).Returns(Task.FromResult(new PatientMedicalFile()));
            _patientRepository.Setup(p => p.GetListOfPatientsSince(new DateTime(2022, 2, 25))).Returns(Task.FromResult(patEncounters));
            _patientRepository.Setup(p => p.GetAllPatientEncounters()).Returns(Task.FromResult(patEncounters));
            _patientRepository.Setup(p => p.GetListOfIsolatedPeople()).Returns(Task.FromResult(patEncounters));
            _patientRepository.Setup(p => p.AddPatientEncounter(new Patient(), new PotentialPatient())).Returns(Task.FromResult(new Patient()));
            _patientRepository.Setup(p => p.AddNewPatientVisit("", new SiteVisit())).Returns(Task.FromResult(new SiteVisit()));
            _patientRepository.Setup(p => p.GetPatientByIdAsync("sample_id")).Returns(Task.FromResult(new Patient()));
            _patientRepository.Setup(p => p.GetPotentialPatientByIdAsync("")).Returns(Task.FromResult(new PotentialPatient()));

            _patientController = new PatientController(_patientRepository.Object);
        }

        public Patient newCreatePatient = new Patient {
            PatientID = "bsdbhj",
            GovtId = "test_id",
            FirstName = "Alice",
            LastName = "Edwards",
            DateOfBirth = new DateTime(2000, 9, 20),
            PhoneNumber = "3452727282",
            Address = new Address { 
                HouseNumber = 6,
                ApartmentNumber = 0,
                Street = "Edwards",
                City = "Test City"
            },
            HouseMembersNumber = 6,
            isCovidPositive = true,
            CreatedOn = DateTime.Now
        };

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

        [TestMethod]
        public async Task AddNewPatints_should_add_new_patient_in_the_system()
        {
            NewPatientRequest newPatientRequest = new NewPatientRequest
            {
                FirstName = newCreatePatient.FirstName,
                LastName = newCreatePatient.LastName,
                GovtID =  newCreatePatient.GovtId,
                Address = newCreatePatient.Address,
                HouseResidentsAmount = newCreatePatient.HouseMembersNumber,
                Email = newCreatePatient.Email,
                PhoneNumber = newCreatePatient.PhoneNumber,
                BirthDate = newCreatePatient.DateOfBirth,
                IsCovidPositive = newCreatePatient.isCovidPositive
            };
            var result = await _patientController.CreatePatient(newPatientRequest);

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
        }

        [TestMethod]
        public async Task GetAllPatintRote_should_return_all_patients_sites_visited_last_seven_days_in_the_system()
        {
            var result = await _patientController.GetListLocations("sample_id");

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(List<NewPatientVisitedSite>), "Returned Object Type is not a type of NewPatientVisitedSite List");

            List<NewPatientVisitedSite>? patients = okResult.Value as List<NewPatientVisitedSite>;

        }


        [TestMethod]
        public async Task GetAllPatientEncounters_should_return_all_patients_encounters_in_the_system()
        {
            var result = await _patientController.GetPatientEncounters("sample_id");

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(List<PatientEncounter>), "Returned Object Type is not a type of PatientEncounter List");

            List<PatientEncounter>? patients = okResult.Value as List<PatientEncounter>;
        }

        [TestMethod]
        public async Task GetAllPatientDetails_should_return_medical_file_for_patient()
        {
            var result = await _patientController.GetAllDeatilsByPatientId("sample_id");

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(PatientMedicalFile), "Returned Object Type is not a type of PatientMedicalFile");

            PatientMedicalFile? patients = okResult.Value as PatientMedicalFile;
        }

        [TestMethod]
        public async Task GetAllSickPatients_should_return_all_sick_patients()
        {
            var result = await _patientController.GetListOfPatientsSince(new DateTime(2022, 2, 25));

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(List<PatientEncounterResponse>), "Returned Object Type is not a type of PatientEncounterResponse");

            List<PatientEncounterResponse>? patients = okResult.Value as List<PatientEncounterResponse>;
        }

        [TestMethod]
        public async Task GetAllPotentialPatients_should_return_all_potential_patients()
        {
            var result = await _patientController.GetListOfPotentialNotInserted();

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(List<PatientEncounterResponse>), "Returned Object Type is not a type of PatientEncounterResponse");

            List<PatientEncounterResponse>? patients = okResult.Value as List<PatientEncounterResponse>;
        }

        [TestMethod]
        public async Task GetAllIsolatedPatients_should_return_all_isolated_patients()
        {
            var result = await _patientController.GetListOfIsolatedPeople();

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
            Assert.IsInstanceOfType(okResult.Value, typeof(List<PatientEncounterResponse>), "Returned Object Type is not a type of PatientEncounterResponse");

            List<PatientEncounterResponse>? patients = okResult.Value as List<PatientEncounterResponse>;
        }
    }
}
