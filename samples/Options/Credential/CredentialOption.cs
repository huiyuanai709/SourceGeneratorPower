using SourceGeneratorPower.Options;

namespace Credential;

[Option("Credential")]
public class CredentialOption
{
    public string ApiKey { get; set; }

    public string AppSecret { get; set; }
    
    [Option("Credential1")]
    public class CredentialOptions1
    {
        public string ApiKey { get; set; }

        public string AppSecret { get; set; }
    }
}