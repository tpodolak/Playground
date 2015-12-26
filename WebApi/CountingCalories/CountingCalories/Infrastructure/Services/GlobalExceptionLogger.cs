using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace CountingCalories.Infrastructure.Services
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalExceptionLogger));

        public GlobalExceptionLogger()
        {
            // TODO refactor later
            log4net.Config.XmlConfigurator.Configure();
        }

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(RequestToString(context.Request), context.Exception);
        }

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return true;
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
                message.Append(request.Method);

            if (request.RequestUri != null)
                message.Append(" ").Append(request.RequestUri);

            return message.ToString();
        }
    }
}