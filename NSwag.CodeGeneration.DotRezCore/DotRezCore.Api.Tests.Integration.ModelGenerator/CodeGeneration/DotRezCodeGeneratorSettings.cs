namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class DotRezCodeGeneratorSettings
    {        
        public string DesiredContractNamespacePrefix { get; set; }
        
        public string CurrentContractNamespacePrefix { get; set; }
        
        public string DesiredClientNamespacePrefix { get; set; }
        
        public string ContractFilesDestinationRoot { get; set; }
        
        public string ClientFilesDestinationRoot { get; set; }
        
        public string ClientName { get; set; }
        
    }
}