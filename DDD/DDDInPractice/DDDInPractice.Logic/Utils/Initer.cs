using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.Management;

namespace DDDInPractice.Logic.Utils
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
            HeadOfficeInstance.Init();
            DomainEvents.Init();
        }
    }
}