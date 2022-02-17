using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand);
    Task<CatalogItem> GetByIdAsync(int id);
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type);
    Task RemoveAsync(int id);
    Task<int?> UpdateAsync(int id, string? name, string? description, decimal? price, int? availableStock, int? catalogBrandId, int? catalogTypeId, string? pictureFileName);
}