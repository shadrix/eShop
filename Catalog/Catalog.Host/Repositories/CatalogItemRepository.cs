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

    public async Task<int?> Create(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.CatalogItems.AddAsync(new CatalogItem
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

    public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brandTitle)
    {
        var resourse = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(ci => ci.CatalogBrand.Brand == brandTitle)
            .ToListAsync();

        return resourse;
    }

    public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(string typeTitle)
    {
        var resourse = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(ci => ci.CatalogType.Type == typeTitle)
            .ToListAsync();

        return resourse;
    }

    public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
    {
        var resourse = await _dbContext.CatalogBrands.ToListAsync();

        return resourse;
    }

    public async Task<IEnumerable<CatalogType>> GetTypesAsync()
    {
        var resourse = await _dbContext.CatalogTypes.ToListAsync();

        return resourse;
    }

    public async Task<CatalogItem> GetByIdAsync(int id)
    {
        var res = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstAsync(ci => ci.Id == id);

        return res;
    }

    public async Task<int> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var itemToUpdate = _dbContext.CatalogItems.First(ci => ci.Id == id);

        itemToUpdate.Name = name;
        itemToUpdate.Description = description;
        itemToUpdate.Price = price;
        itemToUpdate.AvailableStock = availableStock;
        itemToUpdate.CatalogBrandId = catalogBrandId;
        itemToUpdate.CatalogTypeId = catalogTypeId;
        itemToUpdate.PictureFileName = pictureFileName;

        _dbContext.CatalogItems.Update(itemToUpdate);
        await _dbContext.SaveChangesAsync();

        return itemToUpdate.Id;
    }

    public async Task<int> Delete(int id)
    {
        var itemToRemove = _dbContext.CatalogItems.First(ci => ci.Id == id);

        _dbContext.CatalogItems.Remove(itemToRemove);
        await _dbContext.SaveChangesAsync();

        return itemToRemove.Id;
    }
}