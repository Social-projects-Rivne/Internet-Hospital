using AutoFixture;
using AutoMapper;
using FluentAssertions;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace InternetHospital.UnitTests.Doctors
{
    public class DoctorServiceUnitTests: IDisposable
    {
        [Fact]
        public void ShouldAddIllnessHistory()
        {
            // arrange
            var options = DbContextHelper.GetDbOptions(nameof(ShouldAddIllnessHistory));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureAppointment = fixture.Build<Appointment>()
                .Create();

            var illnessHistoryModel = fixture.Build<IllnessHistoryModel>()
                .With(i => i.AppointmentId, fixtureAppointment.Id)
                .Create();

            Mapper.Initialize(cfg => cfg.CreateMap<IllnessHistoryModel, IllnessHistory>());

            using (var context = new ApplicationContext(options))
            {
                context.Appointments.Add(fixtureAppointment);
                context.SaveChanges();

                var doctorService = new DoctorService(context, null, null);

                var (status, message) = doctorService.FillIllnessHistory(illnessHistoryModel);

                status.Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldGetSpecializations()
        {
            // arrange
            var options = DbContextHelper.GetDbOptions(nameof(ShouldAddIllnessHistory));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var specializations = fixture.Build<Specialization>()
                .CreateMany(20)
                .ToList();

            var expectedSpecializations = GetExpectedSpecializations(specializations);

            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, SpecializationModel>());

            using (var context = new ApplicationContext(options))
            {
                context.Specializations.AddRange(specializations);
                context.SaveChanges();

                var doctorService = new DoctorService(context, null, null);

                var result = doctorService.GetAvailableSpecialization();

                result.Should().BeEquivalentTo(expectedSpecializations);
            }
        }

        private List<SpecializationModel> GetExpectedSpecializations(List<Specialization> specializations)
        {
            var expectedDate = Mapper.Map<List<Specialization>, List<SpecializationModel>>(specializations);

            return expectedDate;
        }

        public void Dispose()
        {
            // if we use AutoMapper we must reset it after each test
            Mapper.Reset();
        }
    }
}
