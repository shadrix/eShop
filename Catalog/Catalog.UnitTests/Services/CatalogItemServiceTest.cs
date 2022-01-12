namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task CreateProductAsync_Success()
    {
        // arrange
        var testName = "Name";
        var testDescription = "Description";
        var testPrice = 1000;
        var testavaliableStock = 100;
        var testBrandId = 1;
        var testTypeId = 1;
        var testPictureFileName = "1.png";

        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.Is<string>(i => i == testName),
            It.Is<string>(i => i == testDescription),
            It.Is<decimal>(i => i == testPrice),
            It.Is<int>(i => i == testavaliableStock),
            It.Is<int>(i => i == testBrandId),
            It.Is<int>(i => i == testTypeId),
            It.Is<string>(i => i == testPictureFileName))).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.CreateProductAsync(testName, testDescription, testPrice, testavaliableStock, testBrandId, testTypeId, testPictureFileName);

        // assert
        result.Should().Be(testResult);
    }
}