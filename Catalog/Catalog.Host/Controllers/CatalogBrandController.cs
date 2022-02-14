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
    [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create(CreateBrandRequest request)
    {
        var result = await _catalogBrandService.Create(request.Brand);
        return Ok(new AddBrandResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeleteBrandResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteBrandRequest request)
    {
        var result = await _catalogBrandService.Delete(request.Name);
        return Ok(new DeleteBrandResponse() { IsRemoved = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateBrandResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.Update(request.OldName, request.NewName);
        return Ok(new UpdateBrandResponse() { IsUpdated = result });
    }
}