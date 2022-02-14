using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IMapper mapper,
        ICatalogBrandRepository brandRepository)
        : base(dbContextWrapper, logger)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }

    public async Task<int?> UpdateAsync(UpdateBrandRequest brand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = _mapper.Map<CatalogBrand>(brand);
            return await _brandRepository.AddAsync(result);
        });
    }

    public async Task<int?> AddAsync(CreateBrandRequest brand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = _mapper.Map<CatalogBrand>(brand);
            return await _brandRepository.AddAsync(result);
        });
    }

    public async Task<int?> RemoveAsync(int brandId)
    {
        return await ExecuteSafeAsync(async () => await _brandRepository.RemoveAsync(brandId));
    }
}