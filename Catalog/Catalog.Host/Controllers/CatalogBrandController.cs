using System.Net;
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
    private readonly ICatalogBrandService _brandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService brandService)
    {
        _logger = logger;
        _brandService = brandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync(CreateBrandRequest request)
    {
        var result = await _brandService.AddAsync(request);
        return Ok(new AddBrandResponse<int?>()
        {
            Id = result
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAsync(UpdateBrandRequest request)
    {
        var result = await _brandService.UpdateAsync(request);
        return Ok(new UpdateBrandResponse<int?>()
        {
            Result = result
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveAsync(int brandId)
    {
        var res = await _brandService.RemoveAsync(brandId);
        return Ok(new RemoveBrandResponse<int?>()
        {
            Id = res
        });
    }
}