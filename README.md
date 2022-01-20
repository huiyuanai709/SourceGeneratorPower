# SourceGeneratorPower
.Net Source generator powerful components which used roslyn's SourceGenerator feature

## Components

* [SourceGeneratorPower.Options](#sourcegeneratorpoweroptions)
* [SourceGeneratorPower.HttpClient](#sourcegeneratorpowerhttpClient)

## SourceGeneratorPower.Options

SourceGeneratorPower.Options is used C# roslyn's Source Generator feature to auto inject Options class which marked by OptionAttribute. It is convenient to develop, almost no loss of performance.
### Installation

```C#
dotnet add package SourceGeneratorPower.Options.Abstractions
dotnet add package SourceGeneratorPower.Options.SourceGenerator
```

### Usages

1. Marked Option class with OptionAttribute and Configuration section key
```C#
[Option("Greet")]
public class GreetOption
{
    public string Text { get; set; }
}
```
2. appsettings.json add Greet section
```json
{
  "Greet": {
    "Text": "Hello world!"
  }
}

```

Main project use IServiceCollection extension method AutoInjectOptions
```C#
// .Net 6
builder.Services.AutoInjectOptions(builder.Configuration);

// .Net 5 StartUp.cs
service.AutoInjectOptions(Configuration);
```

Then build solution

See document [auto inject options](docs/features/AutoInjectOptions.md)

### Changelog

v1.1.3
1. Fix namespace conflict with `global::`

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



