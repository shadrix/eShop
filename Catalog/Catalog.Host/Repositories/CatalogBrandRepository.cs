using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int> Create(string title)
        {
            var item = await _dbContext.CatalogBrands.AddAsync(new CatalogBrand
            {
                Brand = title
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int> Delete(int id)
        {
            var itemToRemove = _dbContext.CatalogBrands.First(ci => ci.Id == id);

            _dbContext.CatalogBrands.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync();

            return itemToRemove.Id;
        }

        public async Task<int> Update(int id, string title)
        {
            var itemToUpdate = _dbContext.CatalogTypes.First(ci => ci.Id == id);

            itemToUpdate.Type = title;

            _dbContext.CatalogTypes.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync();

            return itemToUpdate.Id;
        }
    }
}
