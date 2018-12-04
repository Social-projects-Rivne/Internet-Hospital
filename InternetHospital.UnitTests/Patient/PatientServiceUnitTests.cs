using AutoFixture;
using AutoMapper;
using FluentAssertions;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InternetHospital.UnitTests.PatientProfile
{
    public class PatientServiceUnitTests : IDisposable
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
                BirthDate = Convert.ToDateTime(patient.BirthDate),
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

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}
