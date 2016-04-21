namespace DDDInPractice.Logic.Atms
{
    public class AtmDto
    {
        public int Id { get; private set; }
        public decimal Cash { get; private set; }

        public AtmDto(int id, decimal cash)
        {
            Id = id;
            Cash = cash;
        }
    }
}
