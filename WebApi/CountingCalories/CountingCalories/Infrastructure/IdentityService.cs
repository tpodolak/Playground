using System.Threading;

namespace CountingKs.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        public string CurrentUser => Thread.CurrentPrincipal.Identity.Name;
    }
}