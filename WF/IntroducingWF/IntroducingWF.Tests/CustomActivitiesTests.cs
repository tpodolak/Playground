using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel.Syndication;
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

        [Test]
        public void GetFeed_ReturnsCorrectAmountOfResults_Test()
        {
            var feedActivity = new GetFeed();
            var feedUrl = "http://google.com/";
            var inputs = new Dictionary<string, object>
            {
                ["FeedUrl"] = feedUrl
            };

            var feed = WorkflowInvoker.Invoke(feedActivity, inputs);
            Assert.IsNotNull(feed);
            Assert.AreEqual(1, feed.Links.Count);
            Assert.AreEqual(feedUrl, feed.Links.Select(val => val.Uri.AbsoluteUri).Single());
        }
    }
}
