using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> Create(string name);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, string name);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
    }
}
