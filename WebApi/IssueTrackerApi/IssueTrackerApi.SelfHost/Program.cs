using System;
using System.Collections.Generic;
using System.Web.Http.SelfHost;

namespace IssueTrackerApi.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");
            WebApiConfig.Register(config);
            var host = new HttpSelfHostServer(config);
            host.OpenAsync().Wait();
            Console.WriteLine($"IssueApi hosted at: {config.BaseAddress}");
            Console.ReadLine();
        }
    }
}
