using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Create(string title);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, string name);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
    }
}
