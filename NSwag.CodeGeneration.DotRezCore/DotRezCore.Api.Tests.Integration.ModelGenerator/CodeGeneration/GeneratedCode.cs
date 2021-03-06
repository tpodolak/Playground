﻿namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class GeneratedCode
    {
        public string ContractCode { get; }

        public string ClientCode { get; }

        public GeneratedCode(string contractCode, string clientCode)
        {
            ContractCode = contractCode;
            ClientCode = clientCode;
        }
    }
}