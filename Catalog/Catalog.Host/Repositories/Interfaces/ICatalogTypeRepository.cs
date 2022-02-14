using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetTypesAsync();
    Task<int?> UpdateAsync(CatalogType type);
    Task<int?> AddAsync(CatalogType type);
    Task<int?> RemoveAsync(int typeId);
}