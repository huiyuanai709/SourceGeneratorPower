using HttpClientSample.API;
using HttpClientSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController: ControllerBase
{
    private readonly IJsonServerApi _jsonServerApi;

    public TodoController(IJsonServerApi jsonServerApi)
    {
        _jsonServerApi = jsonServerApi;
    }

    [HttpGet("{id}")]
    public async Task<Todo> Get(int id, CancellationToken cancellationToken)
    {
        return await _jsonServerApi.Get(id, cancellationToken);
    }

    [HttpPost]
    public async Task<Todo> Post(CreateTodo createTodo, CancellationToken cancellationToken = default)
    {
        return await _jsonServerApi.Post(createTodo, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<Todo> Put(int id, Todo todo, CancellationToken cancellationToken)
    {
        todo.Id = id;
        return await _jsonServerApi.Put(todo, cancellationToken);
    }

    [HttpPatch("{id}")]
    public async Task<Todo> Patch(int id, Todo todo, CancellationToken cancellationToken)
    {
        return await _jsonServerApi.Patch(id, todo, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<object> Delete(int id, CancellationToken cancellationToken)
    {
        return await _jsonServerApi.Delete(id, cancellationToken);
    }
}