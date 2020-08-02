using Limedika.Data;
using Limedika.Data.Models;
using Limedika.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Limedika.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _dbContext;

        public LogService(
            ApplicationDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Log>> GetLogs()
        {
            return await _dbContext.Logs.ToListAsync();
        }

        public void AddLocationLog(int locationId, LocationActionEnum action)
        {
            var log = new Log
            {
                Date = GetDate(),
                LocationId = locationId,
                Action = action
            };

            _dbContext.Logs.Add(log);
        }
        
        private static DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}