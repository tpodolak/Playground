using System;
using System.Activities;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;

namespace IntroducingWF.CustomActivities
{
    public class GetFeedAsync : AsyncCodeActivity<SyndicationFeed>
    {
        public InArgument<string> FeedUrl { get; set; }
        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FeedUrl.Get(context));
            context.UserState = request;
            return request.BeginGetResponse(callback, state);
        }

        protected override SyndicationFeed EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest) context.UserState;
            var endGetResponse = (HttpWebResponse)request.EndGetResponse(result);

            return SyndicationFeed.Load(XmlReader.Create(endGetResponse.GetResponseStream()));
        }
    }
}