using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ICatalogBrandRepository> _logger;

    public CatalogBrandRepository(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<ICatalogBrandRepository> logger)
    {
        _logger = logger;
        _db = dbContextWrapper.DbContext;
    }

    public async Task<IEnumerable<CatalogBrand>> GetBrandesAsync()
    {
        return await _db.CatalogBrands.ToListAsync();
    }

    public async Task<int?> UpdateAsync(CatalogBrand brand)
    {
        if (brand == null)
        {
            return null;
        }

        var brandNew = await _db.CatalogBrands.FirstOrDefaultAsync(b => b.Id == brand.Id);
        if (brandNew != null)
        {
            brandNew.Brand = brand.Brand;
        }

        return await _db.SaveChangesAsync();
    }

    public async Task<int?> AddAsync(CatalogBrand brand)
    {
        var newBrand = await _db.CatalogBrands.AddAsync(brand);

        await _db.SaveChangesAsync();

        return newBrand.Entity.Id;
    }

    public async Task<int?> RemoveAsync(int brandId)
    {
        var brand = await _db.CatalogBrands.Where(b => b.Id == brandId).FirstOrDefaultAsync();

        if (brand == null)
        {
            return null;
        }

        var result = _db.CatalogBrands.Remove(brand);
        await _db.SaveChangesAsync();

        return result.Entity.Id;
    }
}