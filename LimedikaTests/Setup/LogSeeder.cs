using Limedika.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimedikaTests.Setup
{
    public static class LogSeeder
    {
        public static async Task SeedLogs(DbSet<Log> logs)
        {
            var newLogs = new List<Log>
            {
                new Log { LocationId = 1, Action = LocationActionEnum.Created, Date = DateTime.Today.AddDays(-2) },
                new Log { LocationId = 1, Action = LocationActionEnum.PostCodeUpdated, Date = DateTime.Today.AddDays(-1) },
                new Log { LocationId = 2, Action = LocationActionEnum.Created, Date = DateTime.Today }
            };

            await logs.AddRangeAsync(newLogs);
        }
    }
}