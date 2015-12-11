using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntroducingWF.EmployeeLibrary;
using IntroducingWF.Workflows;
using Parallel = System.Activities.Statements.Parallel;

namespace IntroducingWF
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToConsole("Hello", "World from sequence");
            WriteToConsoleParallel("Hello", "World from parallel");
            var startDate = DateTime.UtcNow;
            var endDateWithVerification = startDate.AddDays(7);
            var endDateWithApproval = startDate.AddDays(1);

            // without flowchart
            RequestVacations(startDate, endDateWithVerification);
            RequestVacations(startDate, endDateWithApproval);
            // with flowchart
            RequestVacationFlowChart(startDate, endDateWithVerification);
            RequestVacationFlowChart(startDate, endDateWithApproval);
            // with flowchart and instance WorkflowInvoker
            RequestVacationFlowChartAsync(startDate, endDateWithApproval).Wait();

            Console.ReadKey();
        }

        private static void RequestVacations(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Requesting vacation from: {startDate:yyyy MMMM dd} to: {endDate:yyyy MMMM dd}");
            var requestVacationTimeFlow = new RequestVacationTime();
            var requestParams = CreateRequestParams(startDate, endDate);

            var response = WorkflowInvoker.Invoke(requestVacationTimeFlow, requestParams);
            object result;
            if (response.TryGetValue("Result", out result))
            {
                Console.WriteLine($"Finished processing vacation request. RequestStatus is {result}");
            }
        }

        private static void RequestVacationFlowChart(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Requesting vacation from: {startDate:yyyy MMMM dd} to: {endDate:yyyy MMMM dd} using FlowChart");
            var requestVacationTimeFlow = new VacationFlowChart();
            var requestParams = CreateRequestParams(startDate, endDate);
            var response = WorkflowInvoker.Invoke(requestVacationTimeFlow, requestParams);
            object result;
            if (response.TryGetValue("Result", out result))
            {
                Console.WriteLine($"Finished processing vacation request using FlowChart. RequestStatus is {result}");
            }
        }

        private static async Task RequestVacationFlowChartAsync(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Requesting vacation async from: {startDate:yyyy MMMM dd} to: {endDate:yyyy MMMM dd} using FlowChart ");
            var requestVacationTimeFlow = new VacationFlowChart();
            var requestParams = CreateRequestParams(startDate, endDate);
            var workflowInvoker = new WorkflowInvoker(requestVacationTimeFlow);

            var response = await Task.Factory.FromAsync((callback, o) => workflowInvoker.BeginInvoke(requestParams, callback, o), workflowInvoker.EndInvoke, null);

            object result;
            if (response.TryGetValue("Result", out result))
            {
                Console.WriteLine($"Finished processing vacation async request using FlowChart. RequestStatus is {result}");
            }
        }

        private static Dictionary<string, object> CreateRequestParams(DateTime startDate, DateTime endDate)
        {
            var request = new VacationRequest
            {
                RequestingEmployee = new Employee
                {
                    Name = "John",
                    Email = "John@John.com",
                    Manager = new Employee
                    {
                        Name = "Manager",
                        Email = "Manager@Manager.com"
                    }
                },
                StartDate = startDate,
                EndDate = endDate
            };
            var dictionary = new Dictionary<string, object> {["Request"] = request };
            return dictionary;
        }

        private static void WriteToConsole(string hello, string world)
        {
            var sequence = new Sequence
            {
                Activities =
                {
                    new WriteLine {Text = hello},
                    new WriteLine {Text = world}
                }
            };

            WorkflowInvoker.Invoke(sequence);
        }

        private static void WriteToConsoleParallel(string hello, string world)
        {
            var parallel = new Parallel
            {
                Branches =
                {
                    new WriteLine {Text = hello},
                    new WriteLine {Text = world},
                }
            };

            WorkflowInvoker.Invoke(parallel);
        }
    }
}
