namespace CountingCalories.Infrastructure.Security
{
    public interface IIdentityService
    {
        string CurrentUser { get; }
    }
}