using System;

namespace AbusingLightBDD
{
    public class PotentialBookingSteps
    {
        private readonly IServiceProvider _provider;

        public PotentialBookingSteps(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void SelectFlight()
        {
        }
    }
}