#pragma warning disable CS8602
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext = null!;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            var brands = await _dbContext.
                CatalogBrands.ToListAsync();
            return brands;
        }

        public async Task<int?> AddAsync(string name)
        {
            var brand = await _dbContext.AddAsync(new CatalogBrand { Brand = name });
            await _dbContext.SaveChangesAsync();
            return brand.Entity.Id;
        }

        public async Task<int?> UpdateAsync(int id, string name)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);
            brand.Brand = name;
            brand = _dbContext.Update(brand).Entity;
            await _dbContext.SaveChangesAsync();
            return brand.Id;
        }

        public async Task RemoveAsync(int id)
        {
            var brand = await _dbContext.
                CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);
            _dbContext.CatalogBrands.Remove(brand!);
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
