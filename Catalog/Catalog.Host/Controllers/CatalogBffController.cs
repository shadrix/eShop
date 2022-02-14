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
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataListResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByBrand(ItemsByBrandRequest request)
    {
        var result = await _catalogService.GetByBrandAsync(request.Brand);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataListResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByType(ItemsByTypeRequest request)
    {
        var result = await _catalogService.GetByTypeAsync(request.Type);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemById(ItemByIdRequest request)
    {
        var result = await _catalogService.GetByIdAsync(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataListResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Brands()
    {
        var result = await _catalogService.GetBrandsAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataListResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Types()
    {
        var result = await _catalogService.GetTypesAsync();
        return Ok(result);
    }
}