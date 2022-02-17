using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
        }

        public async Task AddAsync(string name)
        {
            await ExecuteSafeAsync(async () => await _catalogBrandRepository.AddAsync(name));
        }

        public async Task RemoveAsync(int id)
        {
            await ExecuteSafeAsync(async () => await _catalogBrandRepository.RemoveAsync(id));
        }

        public async Task UpdateAsync(int id, string name)
        {
            await ExecuteSafeAsync(async () => await _catalogBrandRepository.UpdateAsync(id, name));
        }
    }
}
