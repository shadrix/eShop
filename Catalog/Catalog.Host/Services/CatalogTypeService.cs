using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly ICatalogTypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public CatalogTypeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogTypeRepository typeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    public async Task<int?> UpdateAsync(UpdateTypeRequest type)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = _mapper.Map<CatalogType>(type);
            return await _typeRepository.UpdateAsync(result);
        });
    }

    public async Task<int?> AddAsync(CreateTypeRequest type)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = _mapper.Map<CatalogType>(type);
            return await _typeRepository.AddAsync(result);
        });
    }

    public async Task<int?> RemoveAsync(int typeId)
    {
        return await ExecuteSafeAsync(async () => await _typeRepository.RemoveAsync(typeId));
    }
}