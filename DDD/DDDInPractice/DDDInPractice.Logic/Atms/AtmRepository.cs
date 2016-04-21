using System.Collections.Generic;
using System.Linq;
using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.Utils;
using NHibernate;
using NHibernate.Linq;

namespace DDDInPractice.Logic.Atms
{
    public class AtmRepository : Repository<Atm>
    {
        public IReadOnlyList<AtmDto> GetAtmList()
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Query<Atm>()
                    .ToList() // TODO use projections
                    .Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
                    .ToList();
            }
        }
    }
}
