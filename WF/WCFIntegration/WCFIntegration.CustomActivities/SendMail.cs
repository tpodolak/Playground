using System;
using System.Activities;
using System.Net.Mail;

namespace WCFIntegration.CustomActivities
{

    public sealed class SendMail : CodeActivity
    {
        public InArgument<string> ToAddress { get; set; }
        public InArgument<string> FromAddress { get; set; }
        public InArgument<string> Subject { get; set; }
        public InArgument<string> MailBody { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            MailMessage msg = new MailMessage
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

            //            SmtpClient client = new SmtpClient();
            //            client.Send(msg);

        }
    }
}
