using System;
using Limedika.Data.Dtos;
using Limedika.Data.Models;
using Limedika.Services.Interfaces;

namespace Limedika.Services
{
    public class LocationMapperService : ILocationMapperService
    {
        public Location Map(LocationDto locationDto)
        {
            if (locationDto == null) throw new ArgumentNullException(nameof(locationDto));

            return new Location
            {
                Name = locationDto.Name,
                Address = locationDto.Address,
                PostCode = locationDto.PostCode,
                Id = locationDto.Id
            };
        }

        public LocationDto Map(Location location)
        {
            if (location == null) throw new ArgumentNullException(nameof(location));

            return new LocationDto
            {
                Id = location.Id,
                Address = location.Address,
                Name = location.Name,
                PostCode = location.PostCode
            };
        }
    }
}