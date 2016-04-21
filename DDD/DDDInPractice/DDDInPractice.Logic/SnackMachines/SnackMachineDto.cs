namespace DDDInPractice.Logic.SnackMachines
{
    public class SnackMachineDto
    {
        public int Id { get; private set; }
        public decimal MoneyInside { get; private set; }

        public SnackMachineDto(int id, decimal moneyInside)
        {
            Id = id;
            MoneyInside = moneyInside;
        }
    }
}
