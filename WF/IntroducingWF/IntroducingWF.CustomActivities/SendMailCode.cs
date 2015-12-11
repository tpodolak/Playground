using System;
using System.Activities;
using System.Net.Mail;

namespace IntroducingWF.CustomActivities
{

    public sealed class SendMailCode : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ToAddress { get; set; }
        public InArgument<string> FromAddress { get; set; }
        public InArgument<string> Subject { get; set; }
        public InArgument<string> MailBody { get; set; }
        public OutArgument<SmtpStatusCode> Result { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            var msg = new MailMessage
            (
                FromAddress.Get(context),
                ToAddress.Get(context),
                Subject.Get(context),
                MailBody.Get(context)
            );

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Sending email from {msg.From} to {msg.To} with subject {msg.Subject} with body {msg.Body}");
            Console.ForegroundColor = color;
            Result.Set(context, SmtpStatusCode.Ok);
            //            SmtpClient client = new SmtpClient();
            //            client.Send(msg);

        }
    }
}
