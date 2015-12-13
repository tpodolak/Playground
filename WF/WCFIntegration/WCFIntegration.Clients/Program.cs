using System;
using System.Activities;

namespace WCFIntegration.Clients
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("About to start expense service workflow");
            var expenseServiceWorkflow = new ExpenseServiceWorkflow();
            WorkflowInvoker.Invoke(expenseServiceWorkflow);
            Console.WriteLine("Expense service workflow finished");

            Console.WriteLine("About to start manual expense service workflow");
            var expenseServiceManualWorkflow = new ExpenseServiceManualWorkflow();
            WorkflowInvoker.Invoke(expenseServiceManualWorkflow);
            Console.WriteLine("Manual expense service workflow finished");


            Console.ReadKey();
        }
    }
}
