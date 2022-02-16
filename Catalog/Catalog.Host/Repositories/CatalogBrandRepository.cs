using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int?> Create(string title)
        {
            var item = await _dbContext.CatalogBrands.AddAsync(new CatalogBrand
            {
                Brand = title
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);

            if (brand is not null)
            {
                _dbContext.Remove(brand);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Update(int id, string title)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);

            if (brand is not null)
            {
                brand.Brand = title;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            var resourse = await _dbContext.CatalogBrands.ToListAsync();

            return resourse;
        }
    }
}
