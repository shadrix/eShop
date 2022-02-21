using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> AddAsync(string name);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task RemoveAsync(int id);
        Task<int?> UpdateAsync(int id, string name);
    }
}