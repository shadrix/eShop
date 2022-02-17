using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogService;
        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IDbContextTransaction> _dbContextTransaction;

        private readonly CatalogBrand _testBrand = new CatalogBrand()
        {
            Id = 1,
            Brand = "test"
        };

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();

            _dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_dbContextTransaction.Object);

            _catalogService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            await _catalogService.AddAsync(_testBrand.Brand);

            // assert
            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).Throws<Exception>();

            await _catalogService.AddAsync(_testBrand.Brand);

            // assert
            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>())).Throws<Exception>();

            await _catalogService.UpdateAsync(_testBrand.Id, _testBrand.Brand);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            await _catalogService.UpdateAsync(_testBrand.Id, _testBrand.Brand);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_Failed()
        {
            _catalogBrandRepository.Setup(s => s.RemoveAsync(
                It.IsAny<int>())).Throws<Exception>();

            await _catalogService.RemoveAsync(_testBrand.Id);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task RemoveAsync_Success()
        {
            await _catalogService.RemoveAsync(_testBrand.Id);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        }
    }
}
