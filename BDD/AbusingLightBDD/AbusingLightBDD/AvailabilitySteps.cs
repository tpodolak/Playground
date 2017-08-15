using System;

namespace AbusingLightBDD
{
    public class AvailabilitySteps : DotRezSteps
    {
        public AvailabilitySteps(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void OneWayFlight()
        {
        }
        
        public void OneWayFlight2()
        {
        }
    }

    public abstract class DotRezSteps
    {
        protected readonly IServiceProvider _serviceProvider;

        public DotRezSteps(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
