using System.Net;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(ILogger<CatalogBrandController> logger, ICatalogBrandService catalogItemService)
    {
        _logger = logger;
        _catalogBrandService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Create(CreateBrandRequest request)
    {
        var result = await _catalogBrandService.Create(request.Brand);

        return Ok(new AddItemResponse<int>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.Update(request.Id, request.Brand);

        return Ok(new UpdateItemResponse<int>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeleteItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Delete(DeleteBrandRequest request)
    {
        var result = await _catalogBrandService.Delete(request.Id);

        return Ok(new DeleteItemResponse<int>() { Id = result });
    }
}