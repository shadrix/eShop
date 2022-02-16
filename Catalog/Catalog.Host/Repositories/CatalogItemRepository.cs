using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> UpdateAsync(CatalogItem item)
    {
        if (item == null)
        {
            return null;
        }

        var newItem = await _dbContext.CatalogItems.FirstOrDefaultAsync(i => i.Id == item.Id);

        if (newItem != null)
        {
            newItem.Description = item.Description;
            newItem.Name = item.Name;
            newItem.Price = item.Price;
            newItem.AvailableStock = item.AvailableStock;
            newItem.CatalogBrandId = item.CatalogBrandId;
            newItem.CatalogTypeId = item.CatalogTypeId;
            newItem.PictureFileName = item.PictureFileName;
        }

        await _dbContext.SaveChangesAsync();

        return newItem?.Id;
    }

    public async Task<int?> RemoveAsync(int itemId)
    {
        var item = await _dbContext.CatalogItems.Where(i => i.Id == itemId).FirstOrDefaultAsync();

        if (item == null)
        {
            return null;
        }

        var result = _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task<CatalogItem?> GetCatalogItemByIdAsync(int id)
    {
        return await _dbContext.CatalogItems.Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(int brandId)
    {
        return await _dbContext.CatalogItems
            .Where(t => t.CatalogBrandId == brandId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsTypeAsync(int typeId)
    {
        return await _dbContext.CatalogItems
            .Where(t => t.CatalogTypeId == typeId)
            .ToListAsync();
    }
}