using System.Collections.Generic;
using System.Linq;
using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.Utils;
using NHibernate;
using NHibernate.Linq;

namespace DDDInPractice.Logic.SnackMachines
{
    public class SnackMachineRepository : Repository<SnackMachine>
    {
        public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Query<SnackMachine>()
                    .ToList() // TODO use projections
                    .Select(x => new SnackMachineDto(x.Id, x.MoneyInside.Amount))
                    .ToList();
            }
        }
    }
}