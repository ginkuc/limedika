using System.Collections.Generic;
using System.Threading.Tasks;
using Limedika.Data.Dtos;

namespace Limedika.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAll();
        Task Import(IEnumerable<ParsedLocationDto> parsedLocations);
        Task UpdateLocationPostCodes();
    }
}