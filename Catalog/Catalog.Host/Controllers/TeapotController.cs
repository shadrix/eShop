using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class TeapotController : ControllerBase
{
    private readonly ILogger<TeapotController> _logger;

    public TeapotController(ILogger<TeapotController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTeapot")]
    public IEnumerable<Teapot> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Teapot
        {
            Title = "Чайник (для чая)",
            MaxTemperature = 100,
            Type = "для плиты",
            Color = "RED"
        })
            .ToArray();
    }
}