using System;
using System.Threading;
using System.Threading.Tasks;

namespace AbusingLightBDD
{
    public class ActiveBookingSteps
    {
        private readonly IServiceProvider _serviceProvider;

        public ActiveBookingSteps(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBags()
        {
        }
        
        public Task AddBagsAsync()
        {
            return Task.FromResult(1);
        }
        
        public void AddBags2()
        {
            throw new AbandonedMutexException();
        }
    }
}