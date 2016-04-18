using DDDInPractice.Logic;

namespace DDDInPractice.UI
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
        }
    }
}