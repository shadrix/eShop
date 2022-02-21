#pragma warning disable CS8602
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
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

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var types = await _dbContext.
                CatalogTypes.ToListAsync();
            return types;
        }

        public async Task<int?> AddAsync(string name)
        {
            var type = await _dbContext.AddAsync(new CatalogType { Type = name });
            await _dbContext.SaveChangesAsync();
            return type.Entity.Id;
        }

        public async Task<int?> UpdateAsync(int id, string name)
        {
            var type = await _dbContext.CatalogTypes.FirstOrDefaultAsync(f => f.Id == id);
            type.Type = name;
            type = _dbContext.Update(type).Entity;
            await _dbContext.SaveChangesAsync();
            return type.Id;
        }

        public async Task RemoveAsync(int id)
        {
            var type = await _dbContext.
                CatalogTypes.FirstOrDefaultAsync(f => f.Id == id);
            _dbContext.CatalogTypes.Remove(type!);
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
