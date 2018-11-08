using System.Linq;
using AutoFixture;

namespace InternetHospital.UnitTests.TestHelpers
{
    public static class FixtureHelper
    {
        /// <summary>
        /// Creates OmitOnRecursionBehavior as opposite to ThrowingRecursionBehavior.
        /// </summary>
        /// <returns>Fixture instance.</returns>
        public static Fixture CreateOmitOnRecursionFixture()
        { 
            //from https://github.com/AutoFixture/AutoFixture/issues/337
            var fixture = new Fixture();

            fixture.Behaviors
                   .OfType<ThrowingRecursionBehavior>()
                   .ToList()
                   .ForEach(b => fixture.Behaviors.Remove(b));

            //recursionDepth
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
