using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Credential;

namespace OptionsDISample.Controllers;

[ApiController]
[Route("[controller]")]
public class CredentialController : ControllerBase
{
    private readonly IOptions<CredentialOption> _options;
    private readonly IOptions<CredentialOption.CredentialOptions1> _credentialOptions1;

    public CredentialController(IOptions<CredentialOption> options, IOptions<CredentialOption.CredentialOptions1> credentialOptions1)
    {
        _options = options;
        _credentialOptions1 = credentialOptions1;
    }

    [HttpGet]
    public async Task<object> GetCredential()
    {
        await Task.CompletedTask;
        return new
        {
            _options.Value.ApiKey,
            _options.Value.AppSecret
        };
    }
    
    [HttpGet("Credential1")]
    public async Task<object> GetCredential1()
    {
        await Task.CompletedTask;
        return new
        {
            _credentialOptions1.Value.ApiKey,
            _credentialOptions1.Value.AppSecret
        };
    }
}