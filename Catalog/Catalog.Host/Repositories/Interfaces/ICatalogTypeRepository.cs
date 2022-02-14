using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Create(string title);
        Task<bool> Delete(string name);
        Task<bool> Update(string oldName, string newName);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
    }
}
