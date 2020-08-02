using Limedika.Data;
using Limedika.Data.Models;
using Limedika.Services;
using Limedika.Services.Interfaces;
using LimedikaTests.Setup;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace LimedikaTests
{
    public class LogServiceTests
    {
        private readonly ILogService _logService;
        private readonly ApplicationDbContext _dbContext;

        public LogServiceTests()
        {
            var testUtilities = new TestUtilities();
            _dbContext = testUtilities.Context;

            _logService = new LogService(_dbContext);
        }

        [Theory]
        [InlineData(1, LocationActionEnum.Created)]
        [InlineData(2, LocationActionEnum.PostCodeUpdated)]
        public async Task When_AddingLog_Expect_LogAdded(int locationId, LocationActionEnum action)
        {
            _logService.AddLocationLog(locationId, action);
            await _dbContext.SaveChangesAsync();

            var log = await _dbContext.Logs.FirstAsync(l => l.LocationId == locationId && l.Action == action);
            Assert.NotNull(log);
        }

        [Fact]
        public async Task When_ReadingLogs_Expect_AllLogsRead()
        {
            var logs = await _logService.GetLogs();

            Assert.Collection(logs,
                l => Assert.Equal(1, l.LocationId), 
                l => Assert.Equal(1, l.LocationId), 
                l => Assert.Equal(2, l.LocationId)
                );
        }
    }
}