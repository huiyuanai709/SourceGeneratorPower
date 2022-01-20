## SourceGeneratorPower.HttpClient

SourceGeneratorPower.HttpClient is used C# roslyn's Source Generator feature to auto implement HTTP API Caller interface, It depends on IHttpClientFactory to create HttpClient to sending request and receive response with System.Text.Json.
### Installation

```C#
dotnet add package SourceGeneratorPower.HttpClient.Abstractions
dotnet add package SourceGeneratorPower.HttpClient.SourceGenerator
```

### Usages

1. Marked interface with HttpClientAttribute and given HttpClient name
```C#
[HttpClient("JsonServer")]
public interface IJsonServerApi
{
    [HttpGet("/todos/{id}")]
    Task<Todo> Get(int id, CancellationToken cancellationToken = default);
}
```
2. Add implement type and HttpClient to DI container
```C#
builder.Services.AddGeneratedHttpClient();
builder.Services.AddHttpClient("JsonServer", options => options.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));
```

More attribute using in string interpolation

```C#
RequiredServiceAttribute
UsingAttribute
```

Then build solution

### Changelog

v1.1.3
1. Fix namespace conflict with `global::`
