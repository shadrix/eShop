using MVC.Models.Dtos;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly HttpClient _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        var filters = new List<CatalogFilterDto>();

        if (brand.HasValue)
        {
            filters.Add(new CatalogFilterDto()
            {
                Type = CatalogTypeFilter.Brand,
                Value = brand.Value
            });

        }
        
        if (type.HasValue)
        {
            filters.Add(new CatalogFilterDto()
            {
                Type = CatalogTypeFilter.Type,
                Value = type.Value
            });
        }

        var responseString = await _httpClient.PostAsJsonAsync(_settings.Value.CatalogUrl,
            new CatalogPaginatedItemsRequest()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        var result = await responseString.Content.ReadAsStringAsync();

        var catalog = JsonSerializer.Deserialize<Catalog>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return catalog;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        await Task.Delay(300);
        var list = new List<SelectListItem>
        {
            new SelectListItem()
            {
                Value = "0",
                Text = "brand"
            }
        };

        return list;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        await Task.Delay(300);
        var list = new List<SelectListItem>
        {
            new SelectListItem()
            {
                Value = "0",
                Text = "type"
            }
        };

        return list;
    }
}
