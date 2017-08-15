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
        public async Task Test1()
        {
            await Runner.RunAsync(Compose());
        }

        private IExtendedCompositeStepBuilder<ActiveBookingSteps> Compose()
        {
            return ExtendedCompositeStepBuilderPresets.AvailabilityBuilder(null)
                .SelectOneWayFlight()
                .SelectOneWayFlight2()
                .AddAsyncSteps(x => x.SelectFlightAsync2())
                .SelectFlight()
                .AddBagsAsyc()
                .AddBags();
            //  .AddBags2()
            // .Build();
        }

        private async Task SomeStep()
        {
            await Task.Delay(100);
        }
    }
}