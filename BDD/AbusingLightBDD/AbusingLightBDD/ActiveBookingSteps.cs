using System;
using System.Threading;

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
        
        public void AddBags2()
        {
            throw new AbandonedMutexException();
        }
    }
}