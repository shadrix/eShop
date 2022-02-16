using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services.Interfaces
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        public Task<int?> Create(string title)
        {
            return ExecuteSafeAsync(() => _catalogBrandRepository.Create(title));
        }

        public Task<bool> Delete(int id)
        {
            return ExecuteSafeAsync(() => _catalogBrandRepository.Delete(id));
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogBrandRepository.GetBrandsAsync();

                return result.Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList();
            });
        }

        public Task<bool> Update(int id, string name)
        {
            return ExecuteSafeAsync(() => _catalogBrandRepository.Update(id, name));
        }
    }
}
