using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;
using Xunit;

namespace InternetHospital.UnitTests.Patient
{
    public class PatientServiceUnitTests
    {
        [Fact]
        public async void ShouldGetPatientProfile()
        {
            // arrange
            var options = DbContextHelper.GetDbOptions(nameof(ShouldGetPatientProfile));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureUser = fixture.Build<User>()
                .Create();

            using (var context = new ApplicationContext(options))
            {
                context.Users.Add(fixtureUser);
                context.SaveChanges();
            }

            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<User, PatientModel>());

            var fakeManager = FakeGenerator.GetFakeUserManager();
            fakeManager
                .Setup(m => m.FindByIdAsync(fixtureUser.Id.ToString()))
                .Returns(Task.FromResult(GetUser(fixtureUser)));

            using (var context = new ApplicationContext(options))
            {
                var patientService = new PatientService(context, null, fakeManager.Object);

                // act
                var patient = await patientService.GetPatientProfile(fixtureUser.Id);

                // assert
                patient.FirstName.Should().BeEquivalentTo(fixtureUser.FirstName);
            }
        }

        [Fact]
        public async void ShouldGetFilteredIllnessHistories()
        {
            var options = DbContextHelper.GetDbOptions(nameof(ShouldGetFilteredIllnessHistories));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureUser = fixture.Build<User>()
                .Create();
            using (var context = new ApplicationContext(options))
            {
                context.Users.Add(fixtureUser);
                context.SaveChanges();
            }

            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<User, PatientModel>());

            var fakeManager = FakeGenerator.GetFakeUserManager();
            fakeManager
                .Setup(m => m.FindByIdAsync(fixtureUser.Id.ToString()))
                .Returns(Task.FromResult(GetUser(fixtureUser)));

            using (var context = new ApplicationContext(options))
            {
                var patientService = new PatientService(context, null, fakeManager.Object);

                // act
                var filteredIllnessHistories = await patientService.GetFilteredHistories(GetExpectedQuerryParams(), fixtureUser.Id.ToString());

                // assert
                filteredIllnessHistories.count.Should().BeGreaterOrEqualTo(fixtureUser.IllnessHistories.Count);
            }

        }


        public static User GetUser(User patient)
        {
            return new User
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                SecondName = patient.SecondName,
                ThirdName = patient.ThirdName,
                BirthDate = patient.BirthDate,
                PhoneNumber = patient.PhoneNumber
            };
        }

        public static IllnessHistorySearchModel GetExpectedQuerryParams()
        {
            return new IllnessHistorySearchModel
            {
                Page = 1,
                PageCount = 1
            };
        }
    }
}
