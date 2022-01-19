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

### Changelog

v1.1.0
1. Add Project Reference support
2. Add Nested Class support