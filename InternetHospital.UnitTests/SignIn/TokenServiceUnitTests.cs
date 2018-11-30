using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using InternetHospital.BusinessLogic.Models;
using Xunit;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.UnitTests.TestHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Microsoft.Extensions.Logging;

namespace InternetHospital.UnitTests.SignIn
{
    /// <summary>
    /// Represents unit tests for TokenService.
    /// </summary>
    public class TokenServiceUnitTests
    {
        private readonly OptionsWrapper<AppSettings> _configuration;

        public TokenServiceUnitTests()
        {
            _configuration = new OptionsWrapper<AppSettings>(new AppSettings
            {
                JwtKey = "SOME_RANDOM_KEY_DO_NOT_SHARE",
                JwtIssuer = "myDomain",
                JwtExpireMinutes = 25
            });
        }

        [Fact]
        public void ShouldGenerateRefreshToken()
        {
            // arrange
            var options = DbContextHelper.GetDbOptions(nameof(ShouldGenerateRefreshToken));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureUser = fixture.Build<User>()
                .Create();

            using (var context = new ApplicationContext(options))
            {
                context.Users.Add(fixtureUser);
                context.SaveChanges();
            }

            var fakeManager = GetFakeManager();

            using (var context = new ApplicationContext(options))
            {
                var tokenService = new TokenService(context, _configuration, fakeManager.Object);

                // act
                var token = tokenService.GenerateRefreshToken(fixtureUser);

                // assert
                token.Should().NotBeNull();
            }
        }

        [Fact]
        public void ShouldGenerateAccessToken()
        {
            // arrange
            var options = DbContextHelper.GetDbOptions(nameof(ShouldGenerateAccessToken));
            var fixture = FixtureHelper.CreateOmitOnRecursionFixture();

            var fixtureUser = fixture.Build<User>()
                .Create();

            using (var context = new ApplicationContext(options))
            {
                context.Users.Add(fixtureUser);
                context.SaveChanges();
            }

            var fakeManager = GetFakeManager();
            fakeManager.Setup(m => m.GetRolesAsync(fixtureUser))
                .Returns(Task.FromResult(GetUserRole()));

            using (var context = new ApplicationContext(options))
            {
                var tokenService = new TokenService(context, _configuration, fakeManager.Object);

                // act
                var token = tokenService.GenerateAccessToken(fixtureUser);

                // assert
                token.Should().NotBeNull();
            }
        }



        private static Mock<UserManager<User>> GetFakeManager()
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

        private static IList<string> GetUserRole()
        {
            return new List<string> { "Admin" };
        }
    }
}
