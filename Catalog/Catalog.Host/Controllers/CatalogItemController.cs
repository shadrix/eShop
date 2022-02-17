using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateProductRequest request)
    {
        var result = await _catalogItemService.AddAsync(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAsync(UpdateProductRequest request)
    {
        var res = await _catalogItemService.UpdateAsync(request);
        return Ok(new UpdateItemResponse<int?>()
        {
            Result = res
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveAsync(int itemId)
    {
        var res = await _catalogItemService.RemoveAsync(itemId);
        return Ok(new RemoveItemResponse<int?>()
        {
            Id = res
        });
    }
}