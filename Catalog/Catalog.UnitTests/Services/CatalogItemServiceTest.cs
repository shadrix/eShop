using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IDbContextTransaction> _dbContextTransaction;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        _dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).Throws<Exception>();

        await _catalogService.UpdateAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        await _catalogService.UpdateAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_Failed()
    {
        _catalogItemRepository.Setup(s => s.RemoveAsync(
            It.IsAny<int>())).Throws<Exception>();

        await _catalogService.RemoveAsync(_testItem.Id);

        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task RemoveAsync_Success()
    {
        await _catalogService.RemoveAsync(_testItem.Id);

        _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
        _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
    }
}