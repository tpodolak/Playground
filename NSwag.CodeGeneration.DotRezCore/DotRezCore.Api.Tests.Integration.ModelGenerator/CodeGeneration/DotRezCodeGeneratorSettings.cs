namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class DotRezCodeGeneratorSettings
    {        
        public string ContractNamespacePrefix { get; set; }
        
        public string ClientNamespacePrefix { get; set; }
        
        public string ContractFilesDestinationRoot { get; set; }
        
        public string ClientFilesDestinationRoot { get; set; }
        
        public string ClientName { get; set; }
    }
}