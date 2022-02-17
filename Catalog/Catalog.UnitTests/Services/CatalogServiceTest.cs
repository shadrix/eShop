using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(
            _dbContextWrapper.Object,
            _logger.Object,
            _catalogItemRepository.Object,
            _mapper.Object,
            _catalogTypeRepository.Object,
            _catalogBrandRepository.Object);
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
            It.Is<int>(i => i == testPageSize)))
            .ReturnsAsync(pagingPaginatedItemsSuccess);

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
            It.Is<int>(i => i == testPageSize)))
            .Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Success()
    {
        // arrange
        var testItemIdSuccess = 1;
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestNameItemDto"
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestNameItem"
        };

        _catalogItemRepository.Setup(s => s.GetCatalogItemByIdAsync(
                It.Is<int>(i => i == testItemIdSuccess)))
            .ReturnsAsync(catalogItemSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess))))
            .Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(testItemIdSuccess);

        // assert
        result.Should().NotBeNull();
        result.Item.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Failed()
    {
        // arrange
        var testItemIdFailed = 1;

        _catalogItemRepository.Setup(s => s.GetCatalogItemByIdAsync(
                It.Is<int>(i => i == testItemIdFailed)))
            .Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(testItemIdFailed);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_Success()
    {
        // arrange
        var testItemIdSuccess = 1;
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestNameItemDto"
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestNameItem"
        };

        var catalogSuccess = new List<CatalogItem>()
        {
            new CatalogItem()
            {
                Name = "TestNameCatalog"
            }
        };

        _catalogItemRepository.Setup(s => s.GetCatalogItemsByBrandAsync(
                It.Is<int>(i => i == testItemIdSuccess)))
            .ReturnsAsync(catalogSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
                It.Is<int>(i => i.Equals(catalogItemSuccess))))
            .Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsByBrandAsync(testItemIdSuccess);

        // assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_Failed()
    {
        // arrange
        var testItemIdFailed = 1;

        _catalogItemRepository.Setup(s => s.GetCatalogItemsByBrandAsync(
                It.Is<int>(i => i == testItemIdFailed)))
            .Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsByBrandAsync(testItemIdFailed);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByTypeAsync_Success()
    {
        // arrange
        var testItemIdSuccess = 1;
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestNameItemDto"
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestNameItem"
        };

        var catalogSuccess = new List<CatalogItem>()
        {
            new CatalogItem()
            {
                Name = "TestNameCatalog"
            }
        };

        _catalogItemRepository.Setup(s => s.GetCatalogItemsTypeAsync(
                It.Is<int>(i => i == testItemIdSuccess)))
            .ReturnsAsync(catalogSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
                It.Is<int>(i => i.Equals(catalogItemSuccess))))
            .Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsByTypeAsync(testItemIdSuccess);

        // assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByTypeAsync_Failed()
    {
        // arrange
        var testItemIdFailed = 1;

        _catalogItemRepository.Setup(s => s.GetCatalogItemsTypeAsync(
                It.Is<int>(i => i == testItemIdFailed)))
            .Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsByTypeAsync(testItemIdFailed);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Success()
    {
        // arrange
        var brandSuccess = new CatalogBrand()
        {
            Brand = "TestBrand"
        };
        var brandDtoSuccess = new CatalogBrandDto()
        {
            Brand = "TestBrandDto"
        };
        var listBrandSuccess = new List<CatalogBrand>()
        {
            new CatalogBrand()
            {
                Brand = "TestListBrand"
            }
        };

        _catalogBrandRepository.Setup(s => s.GetBrandesAsync())
            .ReturnsAsync(listBrandSuccess);

        _mapper.Setup(s => s.Map<CatalogBrandDto>(
                It.Is<CatalogBrand>(i => i.Equals(brandSuccess))))
            .Returns(brandDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogBrandsAsync();

        // assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Failed()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.GetBrandesAsync())
            .ReturnsAsync((Func<List<CatalogBrand>>)null!);

        // act
        var result = await _catalogService.GetCatalogBrandsAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Success()
    {
        var listTypeSuccess = new List<CatalogType>
        {
            new CatalogType
            {
                Type = "Type"
            }
        };
        var typeSuccess = new CatalogType
        {
            Type = "Type"
        };
        var typeDtoSuccess = new CatalogTypeDto
        {
            Type = "Type"
        };
        _catalogTypeRepository.Setup(s => s.GetTypesAsync())
            .ReturnsAsync(listTypeSuccess);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(b => b.Equals(typeSuccess)))).
            Returns(typeDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogTypesAsync();

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Failed()
    {
        // arrange
        _catalogTypeRepository.Setup(s => s.GetTypesAsync())
            .ReturnsAsync((Func<List<CatalogType>>)null!);

        // act
        var result = await _catalogService.GetCatalogTypesAsync();

        // assert
        result.Should().BeNull();
    }
}