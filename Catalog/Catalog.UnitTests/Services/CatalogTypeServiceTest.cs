using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogTypeService>> _logger;

        private readonly CatalogType _testItem = new CatalogType()
        {
            Id = 1,
            Type = "Type"
        };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogTypeService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(
                _dbContextWrapper.Object,
                _logger.Object,
                _catalogTypeRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.Create(
                It.IsAny<string>()))
                .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Create(_testItem.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.Create(
                It.IsAny<string>()))
                .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Create(_testItem.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Success()
        {
            // arrange
            var testResult = true;

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>()))
                .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Update(_testItem.Id, _testItem.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Failed()
        {
            // arrange
            var testResult = false;

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>()))
                .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Update(_testItem.Id, _testItem.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Success()
        {
            // arrange
            var testResult = true;

            _catalogTypeRepository.Setup(s => s.Delete(
                It.IsAny<int>()))
                .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Delete(_testItem.Id);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            // arrange
            var testResult = false;

            _catalogTypeRepository.Setup(s => s.Delete(
                It.IsAny<int>()))
                .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Delete(_testItem.Id);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task GetTypesAsync_Success()
        {
            // arrange
            var testResult = new List<CatalogType>()
            {
                new CatalogType()
                {
                    Type = "TestName",
                },
            };

            var catalogTypeSuccess = new CatalogType()
            {
                Type = "TestName"
            };

            var catalogTypeDtoSuccess = new CatalogTypeDto()
            {
                Type = "TestName"
            };

            _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogTypeSuccess)))).Returns(catalogTypeDtoSuccess);

            // act
            var result = await _catalogTypeService.GetTypesAsync();

            // assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetTypesAsync_Failed()
        {
            // arrange
            var testResult = new List<CatalogType>();

            _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.GetTypesAsync();

            // assert
            result.Should().HaveCount(0);
        }
    }
}
