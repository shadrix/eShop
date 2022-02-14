using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Create(string name);
        Task<bool> Delete(string name);
        Task<bool> Update(string oldName, string newName);
        Task<IEnumerable<CatalogTypeDto>> GetTypesAsync();
    }
}
