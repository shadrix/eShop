using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Create(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Create(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<bool> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Delete(id));
    }

    public Task<bool> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Update(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }
}