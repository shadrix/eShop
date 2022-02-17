using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(ILogger<CatalogTypeController> logger, ICatalogTypeService catalogTypeService)
    {
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoidResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateTypeRequest request)
    {
        await _catalogTypeService.AddAsync(request.Type);
        return Ok(new VoidResponse());
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoidResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        await _catalogTypeService.UpdateAsync(request.Id, request.Type);
        return Ok(new VoidResponse());
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoidResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveTypeRequest request)
    {
        await _catalogTypeService.RemoveAsync(request.Id);
        return Ok(new VoidResponse());
    }
}