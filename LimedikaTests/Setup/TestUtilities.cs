using Limedika.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LimedikaTests.Setup
{
    public class TestUtilities
    {
        private ApplicationDbContext _context;

        public ApplicationDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetDatabase("TestDatabase");
                    LocationSeeder.SeedLocations(_context.Locations).Wait();
                    LogSeeder.SeedLogs(_context.Logs).Wait();

                    _context.SaveChanges();
                }

                return _context;
            }
        }


        private static ApplicationDbContext GetDatabase(string databaseName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(serviceProvider)
                .EnableSensitiveDataLogging()
                .Options;


            return new ApplicationDbContext(dbContextOptions);
        }
    }
}