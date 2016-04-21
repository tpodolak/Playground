namespace DDDInPractice.Logic.Management
{
    public static class HeadOfficeInstance
    {
        private const int HeadOfficeId = 1;
        public static HeadOffice Instance { get; private set; }

        public static void Init()
        {
            var repo = new HeadOfficeRepository();
            Instance = repo.GetById(HeadOfficeId);
        }
    }
}