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
    private readonly IMapper _mapper;
    private readonly ICatalogTypeRepository _typeRepository;
    private readonly ICatalogBrandRepository _brandRepository;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper,
        ICatalogTypeRepository typeRepository,
        ICatalogBrandRepository brandRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
        _typeRepository = typeRepository;
        _brandRepository = brandRepository;
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

    public async Task<GetByIdResponse<CatalogItemDto>> GetCatalogItemByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetCatalogItemByIdAsync(id);
            return new GetByIdResponse<CatalogItemDto>
            {
                Item = _mapper.Map<CatalogItemDto>(result)
            };
        });
    }

    public async Task<GetBrandsResponse<CatalogItemDto>> GetCatalogItemsByBrandAsync(int brandId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetCatalogItemsByBrandAsync(brandId);
            return new GetBrandsResponse<CatalogItemDto>()
            {
                Data = result.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
            };
        });
    }

    public async Task<GetTypesResponse<CatalogItemDto>> GetCatalogItemsByTypeAsync(int typeId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetCatalogItemsTypeAsync(typeId);
            return new GetTypesResponse<CatalogItemDto>()
            {
                Data = result.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
            };
        });
    }

    public async Task<GetBrandsResponse<CatalogBrandDto>> GetCatalogBrandsAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _brandRepository.GetBrandesAsync();
            return new GetBrandsResponse<CatalogBrandDto>
            {
                Data = result.Select(b => _mapper.Map<CatalogBrandDto>(result)).ToList()
            };
        });
    }

    public async Task<GetTypesResponse<CatalogTypeDto>> GetCatalogTypesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _typeRepository.GetTypesAsync();
            return new GetTypesResponse<CatalogTypeDto>
            {
                Data = result.Select(b => _mapper.Map<CatalogTypeDto>(result)).ToList()
            };
        });
    }
}