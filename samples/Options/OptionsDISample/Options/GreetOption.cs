using SourceGeneratorPower.Options;

namespace OptionsDISample.Options;

[Option("Greet")]
public class GreetOption
{
    public string Text { get; set; }
}