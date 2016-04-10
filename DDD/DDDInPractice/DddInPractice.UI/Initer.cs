using DddInPractice.Logic;

namespace DddInPractice.UI
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
        }
    }
}