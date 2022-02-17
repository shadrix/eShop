using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        ICatalogBrandRepository catalogBrandRepository,
        ICatalogTypeRepository catalogTypeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _catalogTypeRepository = catalogTypeRepository;
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<DataResponse<CatalogItemDto>> GetByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByIdAsync(id);
            return new DataResponse<CatalogItemDto>
            {
                Data = _mapper.Map<CatalogItemDto>(result)
            };
        });
    }

    public async Task<DataListResponse<CatalogItemDto>> GetByBrandAsync(string name)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByBrandAsync(name);
            return new DataListResponse<CatalogItemDto>
            {
                Data = result.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList()
            };
        });
    }

    public async Task<DataListResponse<CatalogItemDto>> GetByTypeAsync(string name)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByTypeAsync(name);
            return new DataListResponse<CatalogItemDto>
            {
                Data = result.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList()
            };
        });
    }

    public async Task<DataListResponse<CatalogBrandDto>> GetBrandsAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBrandRepository.GetBrandsAsync();
            return new DataListResponse<CatalogBrandDto>
            {
                Data = result.Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList()
            };
        });
    }

    public async Task<DataListResponse<CatalogTypeDto>> GetTypesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogTypeRepository.GetTypesAsync();
            return new DataListResponse<CatalogTypeDto>
            {
                Data = result.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList()
            };
        });
    }
}