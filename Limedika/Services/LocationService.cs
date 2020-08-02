using System;
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
        private readonly PostItPostCodeResolver _postCodeResolver;

        public LocationService(
            ApplicationDbContext dbContext,
            ILocationMapperService locationMapper,
            PostItPostCodeResolver postCodeResolver
            )
        {
            _dbContext = dbContext;
            _locationMapper = locationMapper;
            _postCodeResolver = postCodeResolver;
        }

        public async Task<IEnumerable<LocationDto>> GetAll()
        {
            return await _dbContext.Locations
                .Select(l => _locationMapper.Map(l))
                .ToListAsync();
        }

        public async Task Import(IEnumerable<ParsedLocationDto> parsedLocations)
        {
            var locationModels = parsedLocations
                .Select(p => _locationMapper.Map(p));

            await _dbContext.Locations.AddRangeAsync(locationModels);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateLocationPostCodes()
        {
            var failedLocations = new List<string>();
            foreach (var location in _dbContext.Locations)
            {
                try
                {
                    var postCode = await _postCodeResolver.GetPostCode(location.Address);
                    location.PostCode = postCode;
                }
                catch
                {
                    failedLocations.Add(location.Address);
                }
            }

            await _dbContext.SaveChangesAsync();

            if (failedLocations.Any())
            {
                throw new InvalidOperationException($"The following addresses were not updated properly: " +
                                                    $"{string.Join(' ', failedLocations)}");
            }
        }
    }
}