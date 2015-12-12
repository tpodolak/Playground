using System;
using System.Activities;

namespace IntroducingWF.CustomActivities
{
    public class GetManager : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> EmployeeEmailAddress { get; set; }

        public OutArgument<string> ManagerEmail { get; set; }

        public OutArgument<string> ManagerName { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var employee = EmployeeEmailAddress.Get(context);
            if (string.IsNullOrWhiteSpace(employee))
                throw new ArgumentException("Invalid address provided for employee", "EmployeeEmailAddress");

            ManagerEmail?.Set(context, "manager@contoso.com");
            ManagerName?.Set(context, "manager doe");
        }
    }  
}
