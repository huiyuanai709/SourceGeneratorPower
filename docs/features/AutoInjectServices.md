## SourceGeneratorPower.Services

SourceGeneratorPower.Services is used C# roslyn's Source Generator feature to auto inject services which marked by TransientServiceAttribute, ScopedServiceAttribute or SingletonServiceAttribute. It is convenient to develop, almost no loss of performance.

### Installation

```C#
dotnet add package SourceGeneratorPower.Services.Abstractions
dotnet add package SourceGeneratorPower.Services.SourceGenerator
```

### Design

Marked interface or class as specified lifetime service

On Interface:
* Pure Attribute: Scan the interface and all its implementation classes and generate source codes added to the container with the specified lifetime.
* Parameters Attribute: Use the class of the parameters as the implementation class to generate source codes added to the container

On Class:
* Pure Attribute: Add the class as an interface and implementation class to generate source codes into the container.
* Parameters Attribute: Use the interface of the parameters as and self as implement class to generate source codes added to the container.

### Usages

1. Marked Interface or Class with specified lifetime ServiceAttribute
```C#
[SingletonService]
public interface ISingletonGuidGenerator
{
    string Generate();
}
```

Main project use IServiceCollection extension method AddAutoInjectServices
```C#
// .Net 6
builder.Services.AddAutoInjectServices();

// .Net 5 StartUp.cs
service.AddAutoInjectServices();
```

Then build solution
