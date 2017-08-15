namespace AbusingLightBDD
{
    public static class ICompositeStepBuilderExtensions
    {
        public static IExtendedCompositeStepBuilder<AvailabilitySteps> SelectOneWayFlight(
            this IExtendedCompositeStepBuilder<AvailabilitySteps> builder)
        {
            var compositeStepBuilder =
                builder.AddSteps(_ => _.OneWayFlight());

            return
                compositeStepBuilder; //compositeStepBuilder.WithUpdatedContext(() => new PotentialBookingSteps(compositeStepBuilder.ServiceProvider));
        }

        public static IExtendedCompositeStepBuilder<PotentialBookingSteps> SelectOneWayFlight2(
            this IExtendedCompositeStepBuilder<AvailabilitySteps> builder)
        {
            var compositeStepBuilder =
                builder.AddSteps(_ => _.OneWayFlight2());

            return compositeStepBuilder.WithUpdatedContext(() => new PotentialBookingSteps(compositeStepBuilder.ServiceProvider));
        }
        
        public static IExtendedCompositeStepBuilder<ActiveBookingSteps> SelectFlight(
            this IExtendedCompositeStepBuilder<PotentialBookingSteps> builder)
        {
            return builder.AddAsyncSteps(_ => _.SelectFlightAsync()).WithUpdatedContext(() => new ActiveBookingSteps(null));
        }

        public static IExtendedCompositeStepBuilder<ActiveBookingSteps> AddBags(
            this IExtendedCompositeStepBuilder<ActiveBookingSteps> builder)
        {
            return builder.AddSteps(_ => _.AddBags());
        }

        public static IExtendedCompositeStepBuilder<ActiveBookingSteps> AddBagsAsyc(
            this IExtendedCompositeStepBuilder<ActiveBookingSteps> builder)
        {
            return builder.AddSteps(_ => _.AddBagsAsync());
        }
        
        public static IExtendedCompositeStepBuilder<ActiveBookingSteps> AddBags2(
            this IExtendedCompositeStepBuilder<ActiveBookingSteps> builder)
        {
            return builder.AddSteps(_ => _.AddBags2());
        }
    }
}