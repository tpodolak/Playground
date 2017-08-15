using System;
using System.Threading.Tasks;

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
        
        public async Task SelectFlightAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
        
        public async Task SelectFlightAsync2()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}