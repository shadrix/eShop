using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType(typeof(AddItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateProduct(CreateProductRequest request)
    {
        var result = await _catalogItemService.CreateProductAsync(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int>() { Id = result });
    }
}