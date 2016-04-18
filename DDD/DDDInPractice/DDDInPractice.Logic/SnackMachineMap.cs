using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace DDDInPractice.Logic
{
	public class SnackMachineMap : ClassMap<SnackMachine>
	{
		public SnackMachineMap()
		{
		    Id(val => val.Id);
			Component(val => val.MoneyInside, val =>
			{
				val.Map(x => x.OneCentCount);
				val.Map(x => x.TenCentCount);
				val.Map(x => x.QuarterCount);
				val.Map(x => x.OneDollarCount);
				val.Map(x => x.FiveDollarCount);
				val.Map(x => x.TwentyDollarCount);
			});

		    HasMany<Slot>(Reveal.Member<SnackMachine>("Slots")).Not.LazyLoad().Cascade.SaveUpdate();
		}
	}
}