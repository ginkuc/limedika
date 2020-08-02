using Limedika.Data.Dtos;
using Limedika.Data.Models;

namespace Limedika.Services.Interfaces
{
    public interface ILocationMapperService
    {
        Location Map(LocationDto locationDto);
        LocationDto Map(Location location);
    }
}