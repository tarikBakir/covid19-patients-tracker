using covid19_patients_tracker.Controllers;
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
    public  class LabTestControllerUnitTests
    {
        private Mock<ILabTestRepository>? _labTestRepository;
        private LabTestController? _labTestController;

        [TestInitialize]
        public void TestInit()
        {
            _labTestRepository = new Mock<ILabTestRepository>();

            LabTestRequest newLabTest = new LabTestRequest
            {
                TestDate = DateTime.Now,
                LabID = "",
                TestID = "",
                PatientID = "",
                isCovidPositive = true
            };

            LabTest LabTest = new LabTest
            {
                TestDate = DateTime.Now,
                LabID = "",
                TestID = "",
                isCovidPositive = true
            };

            Task<LabTest> taskLabTest = Task.FromResult(LabTest);

            _labTestRepository.Setup(l => l.CreateLabTest(newLabTest)).Returns(taskLabTest);

            _labTestController = new LabTestController(_labTestRepository.Object);
        }

        [TestMethod]
        public async Task LabTest_CreateLabTest()
        {
            LabTestRequest labTest = new LabTestRequest
            {
                TestDate = DateTime.Now,
                LabID = "",
                TestID = "",
                isCovidPositive = true
            };

            var result = await _labTestController.CreatePatientLabTest(labTest);

            var okResult = result as ObjectResult;

            // check if the result in not null and retuning 200 response
            Assert.IsNotNull(okResult, "No Response from the method");
            Assert.IsTrue(okResult is OkObjectResult, "The response in not 200");
        }
    }
}
