using Limedika.Data.Dtos;
using Limedika.Data.Models;
using Limedika.Services;
using Limedika.Services.Interfaces;
using Xunit;

namespace LimedikaTests
{
    public class LocationMapperTests
    {
        private readonly ILocationMapperService _locationMapperService;
        private const int TestId = 3;
        private const string TestAddress = "testAddress";
        private const string TestName = "testName";
        private const string TestPostCode = "12345";

        public LocationMapperTests()
        {
            _locationMapperService = new LocationMapperService();
        }

        [Fact]
        public void When_MappingFrom_Dto_To_Model_Expect_CorrectMapping()
        {
            var locationDto = new LocationDto
            {
                Id = TestId,
                Name = TestName,
                Address = TestAddress,
                PostCode = TestPostCode
            };

            var locationModel = _locationMapperService.Map(locationDto);

            Assert.NotNull(locationModel);
            Assert.Equal(TestAddress, locationModel.Address);
            Assert.Equal(TestName, locationModel.Name);
            Assert.Equal(TestPostCode, locationModel.PostCode);
            Assert.Equal(TestId, locationModel.Id);
        }

        [Fact]
        public void When_MappingFrom_Model_To_Dto_Expect_CorrectMapping()
        {
            var locationModel = new Location
            {
                Id = TestId,
                Name = TestName,
                Address = TestAddress,
                PostCode = TestPostCode
            };

            var locationDto = _locationMapperService.Map(locationModel);

            Assert.NotNull(locationDto);
            Assert.Equal(TestAddress, locationDto.Address);
            Assert.Equal(TestName, locationDto.Name);
            Assert.Equal(TestPostCode, locationDto.PostCode);
            Assert.Equal(TestId, locationDto.Id);
        }

        [Fact]
        public void When_MappingFrom_ParsedDto_To_Model_Expect_CorrectMapping()
        {
            var parsedLocationDto = new ParsedLocationDto
            {
                Name = TestName,
                Address = TestAddress,
                PostCode = TestPostCode
            };

            var locationModel = _locationMapperService.Map(parsedLocationDto);

            Assert.NotNull(locationModel);
            Assert.Equal(TestAddress, locationModel.Address);
            Assert.Equal(TestName, locationModel.Name);
            Assert.Equal(TestPostCode, locationModel.PostCode);
        }
    }
}
