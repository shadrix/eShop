using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public async Task<int?> UpdateAsync(UpdateProductRequest product)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = _mapper.Map<CatalogItem>(product);
            return await _catalogItemRepository.UpdateAsync(result);
        });
    }

    public async Task<int?> RemoveAsync(int itemId)
    {
        return await ExecuteSafeAsync(async () => await _catalogItemRepository.RemoveAsync(itemId));
    }
}