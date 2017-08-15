using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Basic;

namespace AbusingLightBDD
{
    public static class IBddRunnerExtensions
    {
        public static void Run<T>(this IBddRunner runner, IExtendedCompositeStepBuilder<T> compositeStepBuilder)
        {
            runner.RunScenarioAsync(() => Task.FromResult(compositeStepBuilder.Build())).Wait();

            // runner.Run(compositeStepBuilder.Build());
        }
        
        public static async Task RunAsync<T>(this IBddRunner runner, IExtendedCompositeStepBuilder<T> compositeStepBuilder)
        {
            await runner.RunScenarioAsync(() => Task.FromResult(compositeStepBuilder.Build()));

            // runner.Run(compositeStepBuilder.Build());
        }
        
        public static void Run(this IBddRunner runner, CompositeStep compositeStep)
        {
            runner.RunScenarioAsync(() => Task.FromResult(compositeStep)).Wait();
        }
    }
}
