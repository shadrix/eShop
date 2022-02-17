using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Create(string name);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, string name);
        Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync();
    }
}