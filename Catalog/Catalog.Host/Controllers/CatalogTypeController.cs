using System.Net;
using Catalog.Host.Models.Requests.Type;
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
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Create(CreateTypeRequest request)
    {
        var result = await _catalogTypeService.Create(request.Type);

        return Ok(new AddItemResponse<int>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Update(UpdateTypeRequest request)
    {
        var result = await _catalogTypeService.Update(request.Id, request.Type);

        return Ok(new UpdateItemResponse<int>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeleteItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Delete(DeleteTypeRequest request)
    {
        var result = await _catalogTypeService.Delete(request.Id);

        return Ok(new DeleteItemResponse<int>() { Id = result });
    }
}