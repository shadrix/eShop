using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
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

    public CatalogBrandController(ILogger<CatalogBrandController> logger, ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoidResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateBrandRequest request)
    {
        await _catalogBrandService.AddAsync(request.Brand);
        return Ok(new VoidResponse());
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoidResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        await _catalogBrandService.UpdateAsync(request.Id, request.Brand);
        return Ok(new VoidResponse());
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoidResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveBrandRequest request)
    {
        await _catalogBrandService.RemoveAsync(request.Id);
        return Ok(new VoidResponse());
    }
}