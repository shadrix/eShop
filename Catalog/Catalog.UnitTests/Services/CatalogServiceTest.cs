using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IDbContextTransaction> _dbContextTransaction;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        _dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _catalogBrandRepository.Object, _catalogTypeRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Success()
    {
        var testSuccessListBrands = new List<CatalogBrand>()
        {
                new CatalogBrand { Id = 1, Brand = "Test" }
        };

        _catalogBrandRepository.Setup(s => s.GetBrandsAsync())
            .ReturnsAsync(testSuccessListBrands);

        var result = await _catalogService.GetBrandsAsync();
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Failed()
    {
        _catalogBrandRepository.Setup(s => s.GetBrandsAsync())
            .ReturnsAsync((Func<IEnumerable<CatalogBrand>>)null!);

        var result = await _catalogService.GetBrandsAsync();
        result.Should().BeNull();
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Success()
    {
        var testSuccessListTypes = new List<CatalogType>()
        {
                new CatalogType { Id = 1, Type = "Test" }
        };

        _catalogTypeRepository.Setup(s => s.GetTypesAsync())
            .ReturnsAsync(testSuccessListTypes);

        var result = await _catalogService.GetTypesAsync();
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Failed()
    {
        _catalogTypeRepository.Setup(s => s.GetTypesAsync())
            .ReturnsAsync((Func<IEnumerable<CatalogType>>)null!);

        var result = await _catalogService.GetTypesAsync();
        result.Should().BeNull();
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        var testItemSuccess = new CatalogItem
        {
            Id = 1,
            Name = "Test",
            Price = 1,
            AvailableStock = 1,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            CatalogBrand = new CatalogBrand(),
            CatalogType = new CatalogType(),
            Description = "test",
            PictureFileName = "test"
        };

        var testItemDtoSuccess = new CatalogItemDto
        {
            Id = 1,
            Name = "Test",
            Price = 1,
            AvailableStock = 1,
            CatalogBrand = new CatalogBrandDto(),
            CatalogType = new CatalogTypeDto(),
            Description = "test",
        };
        var testItemIdSuccess = 1;
        _catalogItemRepository
            .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(testItemSuccess);
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.IsAny<CatalogItem>()))
            .Returns(testItemDtoSuccess);

        var result = await _catalogService.GetByIdAsync(testItemIdSuccess);

        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(testItemSuccess.Id);
        result.Data.Name.Should().Be(testItemSuccess.Name);
        result.Data.Price.Should().Be(testItemSuccess.Price);
        result.Data.AvailableStock.Should().Be(testItemSuccess.AvailableStock);
        result.Data.CatalogBrand.Should().NotBeNull();
        result.Data.CatalogType.Should().NotBeNull();
        result.Data.Description.Should().Be(testItemSuccess.Description);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        var testItemIdFailed = 1;
        _catalogItemRepository
            .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((CatalogItem)null!);

        var result = await _catalogService.GetByIdAsync(testItemIdFailed);

        result.Data.Should().BeNull();
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task GetByTypeAsync_Success()
    {
        var testTypeStrSuccess = "test";
        _catalogItemRepository
            .Setup(s => s.GetByTypeAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new List<CatalogItem>()
                {
                    new CatalogItem()
                });
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.IsAny<CatalogItem>()))
            .Returns(new CatalogItemDto());

        var result = await _catalogService.GetByTypeAsync(testTypeStrSuccess);
        result.Data.Should().NotBeNull();
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task GetByTypeAsync_Failed()
    {
        var testTypeStrFailed = "test";
        _catalogItemRepository
            .Setup(s => s.GetByTypeAsync(It.IsAny<string>()))
            .ReturnsAsync((List<CatalogItem>)null!);

        var result = await _catalogService.GetByTypeAsync(testTypeStrFailed);
        result.Should().BeNull();
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetByBrandAsync_Success()
    {
        var testBrandStrSuccess = "test";
        _catalogItemRepository
            .Setup(s => s.GetByBrandAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new List<CatalogItem>()
                {
                    new CatalogItem()
                });
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.IsAny<CatalogItem>()))
            .Returns(new CatalogItemDto());

        var result = await _catalogService.GetByBrandAsync(testBrandStrSuccess);
        result.Data.Should().NotBeNull();
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task GetByBrandAsync_Failed()
    {
        var testBrandStrFailed = "test";
        _catalogItemRepository
            .Setup(s => s.GetByBrandAsync(It.IsAny<string>()))
            .ReturnsAsync((List<CatalogItem>)null!);

        var result = await _catalogService.GetByBrandAsync(testBrandStrFailed);
        result.Should().BeNull();
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
    }
}