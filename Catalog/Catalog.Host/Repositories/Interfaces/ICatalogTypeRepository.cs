using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int> Create(string title);
        Task<int> Update(int id, string title);
        Task<int> Delete(int id);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
    }
}
