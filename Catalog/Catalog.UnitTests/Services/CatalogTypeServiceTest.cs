using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogService;
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IDbContextTransaction> _dbContextTransaction;

        private readonly CatalogType _testType = new CatalogType()
        {
            Id = 1,
            Type = "test"
        };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();

            _dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_dbContextTransaction.Object);

            _catalogService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            await _catalogService.AddAsync(_testType.Type);

            // assert
            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            _catalogTypeRepository.Setup(s => s.AddAsync(
               It.IsAny<string>())).Throws<Exception>();

            await _catalogService.AddAsync(_testType.Type);

            // assert
            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            _catalogTypeRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>())).Throws<Exception>();

            await _catalogService.UpdateAsync(_testType.Id, _testType.Type);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            await _catalogService.UpdateAsync(_testType.Id, _testType.Type);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_Failed()
        {
            _catalogTypeRepository.Setup(s => s.RemoveAsync(
                It.IsAny<int>())).Throws<Exception>();

            await _catalogService.RemoveAsync(_testType.Id);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task RemoveAsync_Success()
        {
            await _catalogService.RemoveAsync(_testType.Id);

            _dbContextTransaction.Verify(v => v.RollbackAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(v => v.CommitAsync(CancellationToken.None), Times.Once);
        }
    }
}
