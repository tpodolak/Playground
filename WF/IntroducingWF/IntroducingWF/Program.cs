using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
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
            RequestVacations(DateTime.UtcNow, DateTime.UtcNow.AddDays(7));
            RequestVacations(DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
            Console.ReadKey();
        }

        private static void RequestVacations(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Requesting vacation from: {startDate:yyyy MMMM dd} to: {endDate:yyyy MMMM dd}");
            var requestVacationTimeFlow = new RequestVacationTime();
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

            var response = WorkflowInvoker.Invoke(requestVacationTimeFlow, dictionary);
            object result;
            if (response.TryGetValue("Result", out result))
            {
                Console.WriteLine($"Finished processing vacation request. RequestStatus is {result}");
            }
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
