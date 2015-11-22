namespace CountingCalories.Infrastructure.Security
{
    public class IdentityService : IIdentityService
    {
        public string CurrentUser
        {
            get
            {
#if DEBUG
                return "tpodolak";
#else
                return Thread.CurrentPrincipal.Identity.Name;
#endif

            }
        }
    }
}