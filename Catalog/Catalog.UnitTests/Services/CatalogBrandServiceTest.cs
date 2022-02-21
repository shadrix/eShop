using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.UnitTests.Services;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogBrandService>> _logger;

    public CatalogBrandServiceTest()
    {
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogBrandService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransaction.Object);

        _catalogBrandService = new CatalogBrandService(
            _dbContextWrapper.Object,
            _logger.Object,
            _mapper.Object,
            _catalogBrandRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResultSuccess = 1;
        var catalogBrandSuccess = new CatalogBrand
        {
            Brand = "TestNameBrand"
        };
        var catalogBrandRequestSuccess = new CreateBrandRequest
        {
            Brand = "TestNameBrandRequest"
        };

        _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<CatalogBrand>())).
            ReturnsAsync(testResultSuccess);

        _mapper.Setup(s => s.Map<CreateBrandRequest>(
            It.Is<CatalogBrand>(b => b.Equals(catalogBrandSuccess))))
            .Returns(catalogBrandRequestSuccess);

        // act
        var result = await _catalogBrandService.AddAsync(catalogBrandRequestSuccess);

        // assert
        result.Should().Be(testResultSuccess);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;
        var catalogBrandRequestFailed = new CreateBrandRequest
        {
            Brand = "TestNameBrand"
        };

        _catalogBrandRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogBrand>()))
            .ReturnsAsync(testResult);

        // act
        var result = await _catalogBrandService.AddAsync(catalogBrandRequestFailed);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrage
        int? testResultSuccess = 1;

        _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.IsAny<CatalogBrand>()))
            .ReturnsAsync(testResultSuccess);

        // act
        var result = await _catalogBrandService.UpdateAsync(
            new UpdateBrandRequest
            {
                Brand = "testNameBrand"
            });

        // assert
        result.Should().Be(testResultSuccess);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrage
        int? testResultFailed = null;

        _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.IsAny<CatalogBrand>()))
            .ReturnsAsync(testResultFailed);

        // act
        var result = await _catalogBrandService.UpdateAsync(
            new UpdateBrandRequest
            {
                Brand = "testBrandName"
            });

        // assert
        result.Should().Be(testResultFailed);
    }

    [Fact]
    public async Task RemoveAsync_Success()
    {
        // arrage
        var testResultSuccess = 1;

        _catalogBrandRepository.Setup(s => s.RemoveAsync(
            It.IsAny<int>()))
            .ReturnsAsync(testResultSuccess);

        // act
        var result = await _catalogBrandService.RemoveAsync(testResultSuccess);

        // assert
        result.Should().Be(testResultSuccess);
    }

    [Fact]
    public async Task RemoveAsync_Failed()
    {
        // arrage
        var testIdFailed = 1;
        int? testResultFailed = null;

        _catalogBrandRepository.Setup(s => s.RemoveAsync(
            It.IsAny<int>()))
            .ReturnsAsync(testResultFailed);

        // act
        var result = await _catalogBrandService.RemoveAsync(testIdFailed);

        // assert
        result.Should().Be(testResultFailed);
    }
}