using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> UpdateAsync(CatalogItem item);
    Task<int?> RemoveAsync(int itemId);
    Task<CatalogItem?> GetCatalogItemByIdAsync(int id);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(int brandId);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsTypeAsync(int typeId);
}