using FluentNHibernate.Mapping;

namespace DDDInPractice.Logic.SnackMachines
{
    public class SlotMap : ClassMap<Slot>
    {
        public SlotMap()
        {
            Id(val => val.Id);
            Map(val => val.Position);
            Component(val => val.SnackPile, val =>
            {
                val.Map(x => x.Quantity);
                val.Map(x => x.Price);
                val.References(x => x.Snack).Not.LazyLoad();
            });
            References(val => val.SnackMachine);
        }
    }
}