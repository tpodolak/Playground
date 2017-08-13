using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Basic;

namespace AbusingLightBDD
{
    public static class IBddRunnerExtensions
    {
        public static void Run<T>(this IBddRunner runner, ICompositeStepBuilder<T> compositeStepBuilder)
        {
            runner.Run(compositeStepBuilder.Build());
        }
        
        public static void Run(this IBddRunner runner, CompositeStep compositeStep)
        {
            runner.RunScenarioAsync(() => Task.FromResult(compositeStep)).Wait();
        }
    }
}
