using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public Task<int?> Create(string title)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Create(title));
        }

        public Task<bool> Delete(int id)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Delete(id));
        }

        public Task<bool> Update(int id, string name)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Update(id, name));
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetTypesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogTypeRepository.GetTypesAsync();

                return result.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList();
            });
        }
    }
}
