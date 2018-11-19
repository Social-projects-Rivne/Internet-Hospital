using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Xunit;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;

namespace InternetHospital.UnitTests.Appointments
{
    /// <summary>
    /// Represents unit tests for AppointmentService.
    /// </summary>
    public class AppointmentServiceUnitTests
    {
        [Fact]
        public void ShouldGetMyAppointments()
        {
            // arrange
            const int ALLOWED_STATUS_ID = 1;

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
                var appointmentService = new AppointmentService(context);

                // act
                var appointments = appointmentService.GetMyAppointments(doctorId);

                // assert
                appointments.Should().BeEquivalentTo(expectedData);
            }
        }

        private List<AppointmentModel> GetExpectedAppointments(Appointment apoinment)
        {
            var model = new AppointmentModel
                        {
                            Id = apoinment.Id,
                            UserId = apoinment.UserId,
                            UserFirstName = apoinment.User.FirstName,
                            UserSecondName = apoinment.User.SecondName,
                            Address = apoinment.Address,
                            StartTime = apoinment.StartTime,
                            EndTime = apoinment.EndTime,
                            Status = apoinment.Status.Name
                        };

            return new List<AppointmentModel> { model };
        }
    }
}
