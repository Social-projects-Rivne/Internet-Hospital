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
using Microsoft.Extensions.Options;

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

            var fakeManager = FakeGenerator.GetFakeUserManager();

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
        public async void ShouldGenerateAccessToken()
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

            var fakeManager = FakeGenerator.GetFakeUserManager();
            fakeManager
                .Setup(m => m.GetRolesAsync(fixtureUser))
                .Returns(Task.FromResult(UserRoleToIList("testRole")));

            using (var context = new ApplicationContext(options))
            {
                var tokenService = new TokenService(context, _configuration, fakeManager.Object);

                // act
                var token = await tokenService.GenerateAccessToken(fixtureUser);

                // assert
                token.Should().NotBeNull();
            }
        }

        private static IList<string> UserRoleToIList(string role)
        {
            return new List<string> { role };
        }
    }
}
