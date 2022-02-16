using Catalog.Host.Models.Requests;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogTypeService
{
    Task<int?> UpdateAsync(UpdateTypeRequest type);
    Task<int?> AddAsync(CreateTypeRequest type);
    Task<int?> RemoveAsync(int typeId);
}