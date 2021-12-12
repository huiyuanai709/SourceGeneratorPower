using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionsDISample.Options;

namespace OptionsDISample.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetController : ControllerBase
{
    private readonly IOptions<GreetOption> _options;

    public GreetController(IOptions<GreetOption> options)
    {
        _options = options;
    }

    /// <summary>
    /// Greeting
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<string> Get()
    {
        await Task.CompletedTask;
        return _options.Value.Text;
    }
}