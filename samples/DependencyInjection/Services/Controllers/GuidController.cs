using GuidGenerate;
using Microsoft.AspNetCore.Mvc;

namespace Services.Controllers;

[ApiController]
[Route("[controller]")]
public class GuidController : ControllerBase
{
    private readonly ISingletonGuidGenerator _singletonGuidGenerator;
    private readonly IScopeGuidGenerator _scopeGuidGenerator;
    private readonly ITransientGuidGenerator _transientGuidGenerator;

    public GuidController(ISingletonGuidGenerator singletonGuidGenerator, IScopeGuidGenerator scopeGuidGenerator, ITransientGuidGenerator transientGuidGenerator)
    {
        _singletonGuidGenerator = singletonGuidGenerator;
        _scopeGuidGenerator = scopeGuidGenerator;
        _transientGuidGenerator = transientGuidGenerator;
    }

    [HttpGet("Singleton")]
    public string ListSingletonGuid()
    {
        return _singletonGuidGenerator.Generate();
    }
    
    [HttpGet("Scope")]
    public string ListScopeGuid()
    {
        return _scopeGuidGenerator.Generate();
    }
    
    [HttpGet("Transient")]
    public string ListTransientGuid()
    {
        return _transientGuidGenerator.Generate();
    }
}