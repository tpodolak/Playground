namespace CountingKs.Infrastructure
{
    public interface IIdentityService
    {
        string CurrentUser { get; }
    }
}