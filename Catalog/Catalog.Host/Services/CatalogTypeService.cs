using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public async Task AddAsync(string name)
        {
            await ExecuteSafeAsync(async () => await _catalogTypeRepository.AddAsync(name));
        }

        public async Task UpdateAsync(int id, string name)
        {
            await ExecuteSafeAsync(async () => await _catalogTypeRepository.UpdateAsync(id, name));
        }

        public async Task RemoveAsync(int id)
        {
            await ExecuteSafeAsync(async () => await _catalogTypeRepository.RemoveAsync(id));
        }
    }
}
