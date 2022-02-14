#pragma warning disable CS8602
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

    public async Task<CatalogItem> GetByIdAsync(int id)
    {
        var items = await _dbContext.CatalogItems
           .Include(i => i.CatalogBrand)
           .Include(i => i.CatalogType)
           .FirstOrDefaultAsync();
        items = null!;
        return items;
    }

    public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Where(w => w.CatalogBrand.Brand == brand)
            .ToListAsync();

        return items;
    }

    public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Where(w => w.CatalogBrand.Brand == type)
            .ToListAsync();

        return items;
    }

    public async Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price,
            AvailableStock = availableStock
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> UpdateAsync(
        int id,
        string? name = null,
        string? description = null!,
        decimal? price = null!,
        int? availableStock = null!,
        int? catalogBrandId = null!,
        int? catalogTypeId = null,
        string? pictureFileName = null)
    {
        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstOrDefaultAsync(f => f.Id == id);
        if (name is not null)
        {
        item.Name = name;
        }

        if (description is not null)
        {
        item.Description = description;
        }

        if (price is not null)
        {
        item.Price = (decimal)price;
        }

        if (availableStock is not null)
        {
            item.AvailableStock = (int)availableStock;
        }

        if (catalogBrandId is not null)
        {
            item.CatalogBrandId = (int)catalogBrandId;
        }

        if (pictureFileName is not null)
        {
            item.PictureFileName = pictureFileName;
        }

        item = _dbContext.Update(item!).Entity;
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task RemoveAsync(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(x => x.Id == id);

        _dbContext.CatalogItems.Remove(item!);
        await _dbContext.SaveChangesAsync();
    }
}