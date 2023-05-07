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
    private readonly IGenericService<Guid> _genericService;

    public GuidController(ISingletonGuidGenerator singletonGuidGenerator, IScopeGuidGenerator scopeGuidGenerator, ITransientGuidGenerator transientGuidGenerator, IGenericService<Guid> genericService)
    {
        _singletonGuidGenerator = singletonGuidGenerator;
        _scopeGuidGenerator = scopeGuidGenerator;
        _transientGuidGenerator = transientGuidGenerator;
        _genericService = genericService;
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

    [HttpGet("Generic")]
    public string GenericService()
    {
        return _genericService.GetName();
    }
}