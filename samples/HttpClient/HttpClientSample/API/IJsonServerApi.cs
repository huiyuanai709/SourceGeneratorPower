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
    Task<Todo> Get(int id, CancellationToken cancellationToken = default);

    [HttpPost(("/todos"))]
    Task<Todo> Post(CreateTodo createTodo, CancellationToken cancellationToken = default);

    [HttpPut("/todos/{todo.Id}")]
    Task<Todo> Put(Todo todo, CancellationToken cancellationToken);

    [HttpPatch("/todos/{id}")]
    Task<Todo> Patch(int id, Todo todo, CancellationToken cancellationToken);

    [HttpDelete("/todos/{id}")]
    Task<object> Delete(int id, CancellationToken cancellationToken);
}