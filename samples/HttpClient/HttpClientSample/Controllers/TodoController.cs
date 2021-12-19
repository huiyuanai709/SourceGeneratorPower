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
    public async Task<TodoResponse> Get(int id, CancellationToken cancellationToken)
    {
        return await _jsonServerApi.Get(id, cancellationToken);
    }
}