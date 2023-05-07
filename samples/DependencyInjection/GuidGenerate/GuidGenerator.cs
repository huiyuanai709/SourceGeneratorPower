using SourceGeneratorPower.Services.Attributes;

namespace GuidGenerate;

public interface ISingletonGuidGenerator
{
    string Generate();
}

[ScopedService]
public interface IScopeGuidGenerator
{
    string Generate();
}

[TransientService]
public interface ITransientGuidGenerator
{
    string Generate();
}

[SingletonService]
public class SingletonGuidGenerator : ISingletonGuidGenerator
{
    private readonly Guid _guid;
    
    public SingletonGuidGenerator()
    {
        _guid = Guid.NewGuid();
    }
    
    public string Generate()
    {
        return _guid.ToString();
    }
}

public class ScopeGuidGenerator : IScopeGuidGenerator
{
    private readonly Guid _guid;
    
    public ScopeGuidGenerator()
    {
        _guid = Guid.NewGuid();
    }
    
    public string Generate()
    {
        return _guid.ToString();
    }
}

public class TransientGuidGenerator : ITransientGuidGenerator
{
    private readonly Guid _guid;
    
    public TransientGuidGenerator()
    {
        _guid = Guid.NewGuid();
    }
    
    public string Generate()
    {
        return _guid.ToString();
    }
}

[ScopedService]
public interface IGenericService<T>
{
    string GetName();
}

[ScopedService]
public class GenericService<T> : IGenericService<T>
{
    public string GetName()
    {
        return typeof(T).Name;
    }
}