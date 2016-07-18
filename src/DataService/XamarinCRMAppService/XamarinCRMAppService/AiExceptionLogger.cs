

using System;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace XamarinCRMAppService
{
    public class AiExceptionLogger : ExceptionLogger
    {
        static readonly Lazy<TelemetryClient> _LazyAiClient = new Lazy<TelemetryClient>(() => new TelemetryClient());
        static TelemetryClient _AiClient => _LazyAiClient.Value;

        public override void Log(ExceptionLoggerContext context)
        {
            if (context?.Exception != null)
            {
                _AiClient.TrackException(context.Exception);
            }
            base.Log(context);
        }
    }
}