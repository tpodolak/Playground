using System;
using System.Activities;
using System.Collections.Generic;
using System.Data.Services.Client;

namespace IntroducingWF.CustomActivities
{

    public sealed class GetOData<T> : AsyncCodeActivity<IEnumerable<T>>
    {
        [RequiredArgument]
        public InArgument<Uri> ServiceUrl { get; set; }
        [RequiredArgument]
        public InArgument<string> EntitySetName { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, 
            AsyncCallback callback, object state)
        {
             DataServiceContext ctx = 
               new DataServiceContext(ServiceUrl.Get(context));

           DataServiceQuery<T> query = ctx.CreateQuery<T>(EntitySetName.Get(context));
           context.UserState = query;
           return query.BeginExecute(callback, state);
        }

        protected override IEnumerable<T> EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            DataServiceQuery<T> query = context.UserState as DataServiceQuery<T>;
            return query.EndExecute(result);
        }
        
    }
}
