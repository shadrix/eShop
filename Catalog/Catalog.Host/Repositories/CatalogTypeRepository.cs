using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories.Interfaces
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int> Create(string title)
        {
            var item = await _dbContext.CatalogTypes.AddAsync(new CatalogType
            {
                Type = title
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int> Delete(int id)
        {
            var itemToRemove = _dbContext.CatalogTypes.First(ci => ci.Id == id);

            _dbContext.CatalogTypes.Remove(itemToRemove);
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

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var resourse = await _dbContext.CatalogTypes.ToListAsync();

            return resourse;
        }
    }
}
