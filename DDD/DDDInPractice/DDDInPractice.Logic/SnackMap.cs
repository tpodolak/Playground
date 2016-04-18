using FluentNHibernate.Mapping;

namespace DDDInPractice.Logic
{
    public class SnackMap : ClassMap<Snack>
    {
        public SnackMap()
        {
            Id(val => val.Id);
            Map(val => val.Name);
        }
    }
}