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

        public async Task<int?> Create(string title)
        {
            var item = await _dbContext.CatalogTypes.AddAsync(new CatalogType
            {
                Type = title
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> Delete(string title)
        {
            var type = await _dbContext.CatalogTypes
           .Where(w => w.Type.Equals(title))
           .FirstOrDefaultAsync();

            if (type is not null)
            {
                _dbContext.Remove(type);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Update(string oldName, string newName)
        {
            var type = await _dbContext.CatalogTypes
            .Where(w => w.Type.Equals(oldName))
            .FirstOrDefaultAsync();

            if (type is not null)
            {
                type.Type = newName;

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var resourse = await _dbContext.CatalogTypes.ToListAsync();

            return resourse;
        }
    }
}
