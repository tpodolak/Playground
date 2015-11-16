namespace CountingCalories.Infrastructure
{
    public interface IIdentityService
    {
        string CurrentUser { get; }
    }
}