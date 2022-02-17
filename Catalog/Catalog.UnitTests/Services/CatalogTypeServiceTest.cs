using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.UnitTests.Services;

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogBrandService>> _logger;

    public CatalogTypeServiceTest()
    {
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogBrandService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransaction.Object);

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
        var testResultSuccess = 1;
        var catalogTypeSuccess = new CatalogType
        {
            Type = "brandNameTest"
        };
        var catalogTypeRequestSuccess = new CreateTypeRequest
        {
            Type = "brandNameTest"
        };

        _catalogTypeRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogType>()))
            .ReturnsAsync(testResultSuccess);

        _mapper.Setup(s => s.Map<CreateTypeRequest>(
            It.Is<CatalogType>(b => b.Equals(catalogTypeSuccess))))
            .Returns(catalogTypeRequestSuccess);

        // act
        var result = await _catalogTypeService.AddAsync(catalogTypeRequestSuccess);

        // assert
        result.Should().Be(testResultSuccess);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResultFailed = null;
        var catalogTypeRequestFailed = new CreateTypeRequest
        {
            Type = "brandNameTest"
        };

        _catalogTypeRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogType>()))
            .ReturnsAsync(testResultFailed);

        // act
        var result = await _catalogTypeService.AddAsync(catalogTypeRequestFailed);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrage
        int? testResultSuccess = 1;

        _catalogTypeRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogType>()))
            .ReturnsAsync(testResultSuccess);

        // act
        var result = await _catalogTypeService.UpdateAsync(
            new UpdateTypeRequest
            {
                Type = "testName"
            });

        // assert
        result.Should().Be(testResultSuccess);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrage
        int? testResultFailed = null;

        _catalogTypeRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogType>()))
            .ReturnsAsync(testResultFailed);

        // act
        var result = await _catalogTypeService.UpdateAsync(
            new UpdateTypeRequest
            {
                Type = "testName"
            });

        // assert
        result.Should().Be(testResultFailed);
    }

    [Fact]
    public async Task RemoveAsync_Success()
    {
        // arrage
        var testResultSuccess = 1;

        _catalogTypeRepository.Setup(s => s.RemoveAsync(
            It.IsAny<int>()))
            .ReturnsAsync(testResultSuccess);

        // act
        var result = await _catalogTypeService.RemoveAsync(testResultSuccess);

        // assert
        result.Should().Be(testResultSuccess);
    }

    [Fact]
    public async Task RemoveAsync_Failed()
    {
        // arrage
        var testIdFailed = 1;
        int? testResultFailed = null;

        _catalogTypeRepository.Setup(s => s.RemoveAsync(
            It.IsAny<int>()))
            .ReturnsAsync(testResultFailed);

        // act
        var result = await _catalogTypeService.RemoveAsync(testIdFailed);

        // assert
        result.Should().Be(testResultFailed);
    }
}