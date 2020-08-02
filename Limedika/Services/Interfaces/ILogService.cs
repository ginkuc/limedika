using System.Collections.Generic;
using System.Threading.Tasks;
using Limedika.Data.Models;

namespace Limedika.Services.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetLogs();
        void AddLocationLog(int locationId, LocationActionEnum action);
    }
}