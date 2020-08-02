using Limedika.Data;
using Limedika.Data.Dtos;
using Limedika.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Limedika.Data.Models;

namespace Limedika.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILocationMapperService _locationMapper;
        private readonly IPostCodeResolver _postCodeResolver;
        private readonly ILogService _logService;

        public LocationService(
            ApplicationDbContext dbContext,
            ILocationMapperService locationMapper,
            IPostCodeResolver postCodeResolver,
            ILogService logService
            )
        {
            _dbContext = dbContext;
            _locationMapper = locationMapper;
            _postCodeResolver = postCodeResolver;
            _logService = logService;
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
                .Select(p => _locationMapper.Map(p))
                .ToList();

            await _dbContext.Locations.AddRangeAsync(locationModels);

            foreach (var location in locationModels)
            {
                _logService.AddLocationLog(location.Id, LocationActionEnum.Created);
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("There was an error importing location records.");
            }
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

                    _logService.AddLocationLog(location.Id, LocationActionEnum.PostCodeUpdated);
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