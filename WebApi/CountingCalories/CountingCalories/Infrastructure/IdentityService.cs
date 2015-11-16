using System.Threading;

namespace CountingCalories.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        public string CurrentUser => Thread.CurrentPrincipal.Identity.Name;
    }
}