using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> AddAsync(string name);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
        Task RemoveAsync(int id);
        Task<int?> UpdateAsync(int id, string name);
    }
}