using System;
using System.Activities;
using System.Activities.Statements;
using Parallel = System.Activities.Statements.Parallel;

namespace IntroducingWF
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToConsole("Hello", "World");
            WriteToConsoleParallel("Hello", "World");
            Console.ReadKey();
        }

        private static void WriteToConsole(string hello, string world)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            Console.ForegroundColor = ConsoleColor.Green;
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
