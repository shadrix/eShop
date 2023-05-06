using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ICatalogTypeRepository> _logger;

    public CatalogTypeRepository(ILogger<ICatalogTypeRepository> logger, IDbContextWrapper<ApplicationDbContext> db)
    {
        _logger = logger;
        _db = db.DbContext;
    }

    public async Task<IEnumerable<CatalogType>> GetTypesAsync()
    {
        return await _db.CatalogTypes.ToListAsync();
    }

    public async Task<int?> UpdateAsync(CatalogType type)
    {
        if (type == null)
        {
            return null;
        }

        var newType = await _db.CatalogTypes.FirstOrDefaultAsync(t => t.Id == type.Id);

        if (newType != null)
        {
            newType.Type = type.Type;
        }

        await _db.SaveChangesAsync();

        return newType?.Id;
    }

    public async Task<int?> AddAsync(CatalogType type)
    {
        if (type == null)
        {
            return null;
        }

        var newType = await _db.CatalogTypes.AddAsync(type);
        await _db.SaveChangesAsync();

        return newType.Entity.Id;
    }

    public async Task<int?> RemoveAsync(int typeId)
    {
        var type = await _db.CatalogTypes.Where(t => t.Id == typeId).FirstOrDefaultAsync();

        if (type == null)
        {
            return null;
        }

        var result = _db.CatalogTypes.Remove(type);
        await _db.SaveChangesAsync();

        return result.Entity.Id;
    }
}