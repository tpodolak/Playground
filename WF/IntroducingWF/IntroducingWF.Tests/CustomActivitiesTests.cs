using System.Activities;
using System.Collections.Generic;
using System.Net.Mail;
using IntroducingWF.CustomActivities;
using NUnit.Framework;

namespace IntroducingWF.Tests
{
    [TestFixture]
    public class CustomActivitiesTests
    {
        [Test]
        public void NotifyManager_SendsEmail_Test()
        {
            var resultKey = "emailResult";
            var inputs = new Dictionary<string, object>
            {
                ["employeeEmail"] = "john@john.com"
            };

            var notifyManager = new NotifyManager();

            var result = WorkflowInvoker.Invoke(notifyManager, inputs);
            Assert.IsTrue(result.ContainsKey(resultKey));
            Assert.IsInstanceOf<SmtpStatusCode>(result[resultKey]);
            Assert.AreEqual(SmtpStatusCode.Ok, result[resultKey]);
        }
    }
}
