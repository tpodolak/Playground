using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.Xaml;

namespace WCFIntegration.SelfHost
{

    class Program
    {
        static void Main(string[] args)
        {
            WorkflowServiceHost host = new WorkflowServiceHost(new ExpenseService(), new Uri("http://localhost:8731/Samples/Expenses"),
                new Uri("net.tcp://localhost:8732/Samples/Expenses"));
            host.Open();
            Console.WriteLine("Listening");
            Console.ReadKey();
            host.Close();
        }
    }
}
