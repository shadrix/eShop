using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<CatalogItem> GetByIdAsync(int id);
    Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brandTitle);
    Task<IEnumerable<CatalogItem>> GetByTypeAsync(string typeTitle);
    Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
    Task<IEnumerable<CatalogType>> GetTypesAsync();
    Task<int?> Create(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int> Delete(int id);
}