using Limedika.Data.Dtos;
using Limedika.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Limedika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locations = await _locationService.GetAll();

            return Ok(locations);
        }

        [HttpPost]
        public async Task<IActionResult> ImportLocationRecords(ICollection<ParsedLocationDto> locationDtos)
        {
            try
            {
                await _locationService.Import(locationDtos);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateLocationPostCodes()
        {
            try
            {
                await _locationService.UpdateLocationPostCodes();

                return Ok();
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}