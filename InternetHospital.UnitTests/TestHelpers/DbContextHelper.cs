using Microsoft.EntityFrameworkCore;
using InternetHospital.DataAccess;

namespace InternetHospital.UnitTests.TestHelpers
{
    public static class DbContextHelper
    {
        public static DbContextOptions<ApplicationContext> GetDbOptions(string inMemoryDbName)
        {
            return new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: inMemoryDbName)
                .Options;
        }
    }
}
