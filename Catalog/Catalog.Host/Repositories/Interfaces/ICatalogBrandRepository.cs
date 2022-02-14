using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> Create(string name);
        Task<bool> Delete(string name);
        Task<bool> Update(string oldName, string newName);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
    }
}
