using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Basic;
using LightBDD.Framework.Scenarios.Contextual;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using Xunit.Abstractions;

[assembly: LightBddScope]

namespace AbusingLightBDD
{
    public class UnitTest1 : FeatureFixture
    {
        public UnitTest1(ITestOutputHelper output) : base(output)
        {
        }

        [Label("Story-6")]
        [Scenario]
        public void Test1()
        {
            Compose().Run(Runner);
        }

        private CompositeStep Compose()
        {
            return ExtendedCompositeStepBuilderPresets.AvailabilityBuilder(null)
                .SelectOneWayFlight()
                .SelectFlight()
                .AddBags()
              //  .AddBags2()
                .Build();
        }
    }
}