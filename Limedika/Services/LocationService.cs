using Limedika.Data;
using Limedika.Data.Dtos;
using Limedika.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limedika.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILocationMapperService _locationMapper;

        public LocationService(
            ApplicationDbContext dbContext,
            ILocationMapperService locationMapper
            )
        {
            _dbContext = dbContext;
            _locationMapper = locationMapper;
        }

        public async Task<IEnumerable<LocationDto>> GetAll()
        {
            return await _dbContext.Locations
                .Select(l => _locationMapper.Map(l))
                .ToListAsync();
        }
    }
}