using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace DotRezCore.Api.Tests.Integration
{
    public class IntegrationTestsFixture
    {
        private static Lazy<TestServer> ServerProxy => new Lazy<TestServer>(() => new TestServer(new WebHostBuilder().UseStartup<Startup>()));

        public static TestServer Server => ServerProxy.Value;
    }
}