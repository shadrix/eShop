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
    private readonly ICatalogTypeService _typeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService typeService)
    {
        _logger = logger;
        _typeService = typeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync(CreateTypeRequest request)
    {
        var res = await _typeService.AddAsync(request);
        return Ok(new AddTypeResponse<int?>() { Id = res });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAsync(UpdateTypeRequest request)
    {
        var res = await _typeService.UpdateAsync(request);
        return Ok(new UpdateTypeResponse<int?>()
        {
            Result = res
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveAsync(int typeId)
    {
        var res = await _typeService.RemoveAsync(typeId);
        return Ok(new RemoveTypeResponse<int?>()
        {
            Id = res
        });
    }
}