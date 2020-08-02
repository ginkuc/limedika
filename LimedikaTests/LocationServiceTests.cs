using Limedika.Data;
using Limedika.Data.Dtos;
using Limedika.Services;
using Limedika.Services.Interfaces;
using LimedikaTests.Setup;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LimedikaTests
{
    public class LocationServiceTests
    {
        private readonly ILocationService _locationService;
        private readonly ApplicationDbContext _dbContext;

        public LocationServiceTests()
        {
            var testUtilities = new TestUtilities();
            _dbContext = testUtilities.Context;

            var postCodeResolverMock = new Mock<PostItPostCodeResolver>();
            postCodeResolverMock.Setup(m => m.GetPostCode(It.IsAny<string>()))
                .Returns(Task.FromResult("mockPostCode"));

            _locationService = new LocationService(
                _dbContext,
                new LocationMapperService(),
                postCodeResolverMock.Object
                );    
        }

        [Fact]
        public async Task When_GettingAllLocations_Expect_AllLocationsReturned()
        {
            var locations = await _locationService.GetAll();

            Assert.Collection(locations,
                l => Assert.Equal(1, l.Id),
                l => Assert.Equal(2, l.Id),
                l => Assert.Equal(3, l.Id),
                l => Assert.Equal(4, l.Id));
        }

        [Fact]
        public async Task When_UpdatingLocationPostCodes_Expect_PostCodesUpdated()
        {
            await _locationService.UpdateLocationPostCodes();

            var firstLocation = await _dbContext.Locations.FirstAsync();

            Assert.Equal("mockPostCode", firstLocation.PostCode);
        }

        [Fact]
        public async Task When_ImportingLocations_Expect_LocationsImported()
        {
            var parsedLocations = new List<ParsedLocationDto>
            {
                new ParsedLocationDto {Address = "testAddress1", Name = "testName1", PostCode = "11111"},
                new ParsedLocationDto {Address = "testAddress2", Name = "testName2", PostCode = "22222"},
                new ParsedLocationDto {Address = "testAddress3", Name = "testName3", PostCode = "33333"}
            };

            await _locationService.Import(parsedLocations);

            var firstParsedLocation = await _dbContext.Locations.FirstAsync(l => l.Name == "testName1");
            Assert.NotNull(firstParsedLocation);
            var secondParsedLocation = await _dbContext.Locations.FirstAsync(l => l.Name == "testName2");
            Assert.NotNull(secondParsedLocation);
            var thirdParsedLocation = await _dbContext.Locations.FirstAsync(l => l.Name == "testName3");
            Assert.NotNull(thirdParsedLocation);
        }
    }
}