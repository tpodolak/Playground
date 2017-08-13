using System;
using LightBDD.Framework;

namespace AbusingLightBDD
{
    public static class ExtendedCompositeStepBuilderPresets
    {
        public static IExtendedCompositeStepBuilder<AvailabilitySteps> AvailabilityBuilder(IServiceProvider serviceProvider)
        {
            return ExtendedCompositeStepBuilder<AvailabilitySteps>.Construct(serviceProvider,
                provider => new AvailabilitySteps(provider));
        }

        public static IExtendedCompositeStepBuilder<PotentialBookingSteps> PotentialBookingBuilder(
            IServiceProvider serviceProvider)
        {
            return ExtendedCompositeStepBuilder<PotentialBookingSteps>.Construct(serviceProvider,
                provider => new PotentialBookingSteps(provider));
        }

        public static void Run(this CompositeStep step, IBddRunner runner)
        {
            runner.Run(step);
        }
    }
}
