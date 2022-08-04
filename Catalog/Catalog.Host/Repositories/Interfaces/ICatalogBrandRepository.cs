using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<IEnumerable<CatalogBrand>> GetBrandesAsync();
    Task<int?> UpdateAsync(CatalogBrand brand);
    Task<int?> AddAsync(CatalogBrand brand);
    Task<int?> RemoveAsync(int brandId);
}