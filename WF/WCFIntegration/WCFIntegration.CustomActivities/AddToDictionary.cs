using System.Activities;
using System.Collections.Generic;

namespace WCFIntegration.CustomActivities
{
    public sealed class AddToDictionary<TKey, TValue> : CodeActivity
    {
        public InArgument<TValue> Value { get; set; }
        public InArgument<TKey> Key { get; set; }
        public InArgument<Dictionary<TKey, TValue>> Dictionary { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            TKey k = Key.Get(context);
            TValue v = Value.Get(context);
            Dictionary<TKey, TValue> dict = Dictionary.Get(context);

            if (dict != null)
            {
                dict[k] = v;
            }
        }
    }
}