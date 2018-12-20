using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Xunit;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;
using AutoMapper;
using System;
using InternetHospital.BusinessLogic.Interfaces;
using Moq;

namespace InternetHospital.UnitTests.Appointments
{
    /// <summary>
    /// Represents unit tests for AppointmentService.
    /// </summary>
    public class AppointmentServiceUnitTests : IDisposable
    {
       Mock<INotificationService> notificationService;

        public AppointmentServiceUnitTests()
        {
            notificationService = new Mock<INotificationService>();             
        }

        [Fact]
        public void ShouldGetMyAppointments()
        {
            // arrange
            const int ALLOWED_STATUS_ID = 2;

            // get in memory db options
            var options = DbContextHelper.GetDbOptions(nameof(ShouldGetMyAppointments));

            // create fixture 
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            // create test appointment for test case
            var fixtureAppointment = fixture.Build<Appointment>()
                                            .With(a => a.StatusId, ALLOWED_STATUS_ID)
                                            .With(a => a.Status, new AppointmentStatus
                                            {
                                                Id = ALLOWED_STATUS_ID
                                            })
                                            .With(a => a.UserId, 1)
                                            .With(a => a.User, new User
                                            {
                                                Id = 1
                                            })
                                            .Create();

            var expectedData = GetExpectedAppointments(fixtureAppointment);

            // add data to in memory database
            using (var context = new ApplicationContext(options))
            {
                context.Appointments.Add(fixtureAppointment);
                context.SaveChanges();
            }

            // doctor id should be existing
            var doctorId = fixtureAppointment.DoctorId;

            // use a clean instance of the context to run the test
            using (var context = new ApplicationContext(options))
            {
                // create service with generated data
                var appointmentService = new AppointmentService(context, notificationService.Object);

                // act
                var appointments = appointmentService.GetMyAppointments(doctorId);

                // assert
                appointments.Should().BeEquivalentTo(expectedData);
            }
        }

        [Fact]
        public void ShouldGetPatientsAppointments()
        {
            // arrange
            const int ALLOWED_STATUS_ID = 2;
            var options = DbContextHelper.GetDbOptions(nameof(ShouldGetPatientsAppointments));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureAppointment = fixture.Build<Appointment>()
                .With(a => a.StatusId, ALLOWED_STATUS_ID)
                .With(a => a.Status, new AppointmentStatus
                {
                    Id = ALLOWED_STATUS_ID
                })
                .With(a => a.UserId, 1)
                .With(a => a.User, new User
                {
                    Id = 1
                })
                .Create();

            var expectedData = GetExpectedPatientAppointments(fixtureAppointment);

            using (var context = new ApplicationContext(options))
            {
                context.Appointments.Add(fixtureAppointment);
                context.SaveChanges();
            }

            var patientId = fixtureAppointment.UserId;

            using (var context = new ApplicationContext(options))
            {
                var appointmentService = new AppointmentService(context, notificationService.Object);

                // act
                var appointments = appointmentService.GetPatientsAppointments((int)patientId);

                // assert
                appointments.Should().BeEquivalentTo(expectedData);
            }
        }

        [Fact]
        public void ShouldAddAppointment()
        {
            // arrange
            var options = DbContextHelper.GetDbOptions(nameof(ShouldAddAppointment));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureAppointment = fixture.Build<AppointmentCreationModel>()
                .Create();

            var fixtureDoctor = fixture.Build<Doctor>()
                .Create();

            var doctorId = fixtureDoctor.UserId;


            Mapper.Initialize(cfg => cfg.CreateMap<AppointmentCreationModel, Appointment>());

            using (var context = new ApplicationContext(options))
            {
                context.Doctors.Add(fixtureDoctor);
                context.SaveChanges();
            }

            using (var context = new ApplicationContext(options))
            {
                var appointmentService = new AppointmentService(context, notificationService.Object);

                // act
                var status = appointmentService.AddAppointment(fixtureAppointment, doctorId);

                // assert
                status.Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldDeleteAppointment()
        {
            // arrange
            const int ALLOWED_STATUS_ID = 1;
            const int DOCTOR_ID = 1;
            var options = DbContextHelper.GetDbOptions(nameof(ShouldDeleteAppointment));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureAppointment = fixture.Build<Appointment>()
                                           .With(a => a.StatusId, ALLOWED_STATUS_ID)
                                           .With(a => a.Status, new AppointmentStatus
                                           {
                                               Id = ALLOWED_STATUS_ID
                                           })
                                           .With(a => a.DoctorId, DOCTOR_ID)
                                           .With(a => a.Doctor, new Doctor
                                           {
                                               UserId = DOCTOR_ID
                                           })
                                           .Create();

            var appointmentId = fixtureAppointment.Id;
            using (var context = new ApplicationContext(options))
            {
                context.Appointments.Add(fixtureAppointment);
                context.SaveChanges();
            }

            using (var context = new ApplicationContext(options))
            {
                var appointmentService = new AppointmentService(context, notificationService.Object);

                // act
                var appointments = appointmentService.DeleteAppointment(appointmentId, DOCTOR_ID);

                // assert
                appointments.status.Should().BeTrue();
            }
        }

        private List<AppointmentModel> GetExpectedAppointments(Appointment appointment)
        {
            var model = new AppointmentModel
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                UserFirstName = appointment.User.FirstName,
                UserSecondName = appointment.User.SecondName,
                Address = appointment.Address,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Status = appointment.Status.Name
            };

            return new List<AppointmentModel> { model };
        }

        private List<AppointmentForPatient> GetExpectedPatientAppointments(Appointment appointment)
        {
            var model = new AppointmentForPatient
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                DoctorFirstName = appointment.Doctor.User.FirstName,
                DoctorSecondName = appointment.Doctor.User.SecondName,
                DoctorSpecialication = appointment.Doctor.Specialization.Name,
                Address = appointment.Address,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Status = appointment.Status.Name
            };

            return new List<AppointmentForPatient> { model };
        }

        public void Dispose()
        {
            // if we use AutoMapper we must reset it after each test
            Mapper.Reset();
        }
    }
}