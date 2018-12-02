using AutoFixture;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IO;

namespace InternetHospital.UnitTests.TestHelpers
{
    public static class RegistrationHelper
    {
        const int NEW_USER_STATUS = 2;

        public static Mock<UserManager<User>> GetFakeUserManager()
        {
            return new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object);
        }

        public static Mock<UserManager<User>> GetConfiguredUserManager()
        {
            var fakeUserManager = GetFakeUserManager();
            fakeUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            fakeUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            return fakeUserManager;
        }

        public static Mock<RoleManager<IdentityRole<int>>> GetFakeRoleManager()
        {
            return new Mock<RoleManager<IdentityRole<int>>>(
                    new Mock<IRoleStore<IdentityRole<int>>>().Object,
                    new IRoleValidator<IdentityRole<int>>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<ILogger<RoleManager<IdentityRole<int>>>>().Object);
        }

        public static Mock<IFormFile> GetFakeFile()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Fake File";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock;
        }

        public static User GetNewPatient(Fixture fixture)
        {
            return fixture.Build<User>()
                .With(p => p.StatusId, NEW_USER_STATUS)
                .With(p => p.Status, new Status
                {
                    Id = 2,
                    Name = "New"
                })
                .With(p => p.Doctor, null)
                .Create();
        }

        public static User GetNewDoctor(Fixture fixture)
        {
            return fixture.Build<User>()
                .With(u => u.Id, 2)
                .With(u => u.StatusId, NEW_USER_STATUS)
                .With(u => u.Status, new Status
                {
                    Id = 2,
                    Name = "New"
                })
                .With(u => u.Doctor, new Doctor
                {
                    UserId = 2
                })
                .Create();
        }

        public static UserRegistrationModel GetRegistrationModel(User user, string role)
        {
            return new UserRegistrationModel
            {
                Email = user.Email,
                UserName = user.UserName,
                Role = role,
                Password = "1234Pass",
                ConfirmPassword = "1234Pass"
            };
        }
    }
}
