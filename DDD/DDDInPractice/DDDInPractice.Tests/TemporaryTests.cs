﻿using DDDInPractice.Logic;
using DDDInPractice.Logic.Management;
using DDDInPractice.Logic.SharedKernel;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.Logic.Utils;
using Xunit;

namespace DDDInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact(Skip = "Database not ready on CI")]
        public void Test()
        {
            SessionFactory.Init(@"Data Source=DESKTOP-OBST7NQ;Initial Catalog=DDDInPractice;Integrated Security=True");
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var id = 1;
                    var result = session.Get<SnackMachine>(1);
                    var headoffice = session.Get<HeadOffice>(1);
                    result.InsertMoney(Money.Dollar);
                    result.BuySnack(1);
                    session.SaveOrUpdate(result);
                    transaction.Commit();
                }
            }
        }
    }
}
