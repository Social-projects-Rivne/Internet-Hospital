using FluentAssertions;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace InternetHospital.UnitTests.SignUp
{
    public class SignUpServiceUnitTests
    {
        const int NEW_USER_STATUS = 2;
        const string DOCTOR = "Doctor";
        const string PATIENT = "Patient";

        [Fact]
        public async void ShouldCheckSignUpModel()
        {
            var options = DbContextHelper.GetDbOptions(nameof(ShouldCheckSignUpModel)); 
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            // create test objects for test case
            var fixturePatient = RegistrationHelper.GetNewPatient(fixture);
            var fixtureDoctor = RegistrationHelper.GetNewDoctor(fixture);

            var fakeUserManager = RegistrationHelper.GetFakeUserManager();
            
            var expectedPatientModel = RegistrationHelper.GetRegistrationModel(fixturePatient, PATIENT);
            var expectedDoctorModel = RegistrationHelper.GetRegistrationModel(fixtureDoctor, DOCTOR);

            var fakeRegistrationService = new Mock<IRegistrationService>();
            fakeRegistrationService.Setup(s => s.DoctorRegistration(It.IsAny<UserRegistrationModel>()))
                .ReturnsAsync(fixtureDoctor);
            fakeRegistrationService.Setup(s => s.PatientRegistration(It.IsAny<UserRegistrationModel>()))
                .ReturnsAsync(fixturePatient);

            var expectedFile = RegistrationHelper.GetFakeFile();
            var fakeFileService = new Mock<IFilesService>();
            fakeFileService.Setup(s => s.UploadAvatar(It.IsAny<IFormFile>(), It.IsAny<User>()))
                .ReturnsAsync(new User());

            // use a clean instance of the context to run the test
            using (var context = new ApplicationContext(options))
            {
                var signUpService = new SignUpService(fakeRegistrationService.Object, fakeFileService.Object);

                // act
                var isPatient = await signUpService.CheckSignUpModel(fakeUserManager.Object, expectedPatientModel, expectedFile.Object);
                var isDoctor = await signUpService.CheckSignUpModel(fakeUserManager.Object, expectedDoctorModel, expectedFile.Object);

                // assert
                isPatient.state.Should().BeTrue();
                isDoctor.state.Should().BeTrue();
            }
        }
    }
}
