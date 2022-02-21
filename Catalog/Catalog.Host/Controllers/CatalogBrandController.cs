using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogBrandService _catalogService;

    public CatalogBrandController(
        ILogger<CatalogBffController> logger,
        ICatalogBrandService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }
}