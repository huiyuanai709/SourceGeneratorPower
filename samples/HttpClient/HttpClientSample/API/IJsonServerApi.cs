using HttpClientSample.Models;
using HttpClientSample.Options;
using Microsoft.Extensions.Options;
using SourceGeneratorPower.HttpClient;
using SourceGeneratorPower.HttpClient.HttpMethod;

namespace HttpClientSample.API;

[RequiredService(typeof(IOptions<AuthorizationOptions>), "_authOptions")]
[HttpClient("JsonServer")]
public interface IJsonServerApi
{
    [HttpGet("/todos/{id}")]
    Task<TodoResponse> Get([Ignore] int id, CancellationToken cancellationToken = default);
}