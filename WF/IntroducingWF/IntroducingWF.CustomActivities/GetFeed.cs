using System;
using System.Activities;
using System.ServiceModel.Syndication;

namespace IntroducingWF.CustomActivities
{
    public class GetFeed : CodeActivity<SyndicationFeed>
    {
        [RequiredArgument]
        public InArgument<string> FeedUrl { get; set; }

        protected override SyndicationFeed Execute(CodeActivityContext context)
        {
            return new SyndicationFeed("title", "description", new Uri(FeedUrl.Get(context)));
        }
    }
}