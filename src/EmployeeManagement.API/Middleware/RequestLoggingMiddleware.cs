using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            var correlationId = context.Request.Headers.ContainsKey("X-Correlation-ID")
                ? context.Request.Headers["X-Correlation-ID"].ToString()
                : context.TraceIdentifier;

            context.Response.Headers["X-Correlation-ID"] = correlationId;

            using (_logger.BeginScope("CorrelationId:{CorrelationId}", correlationId))
            {
                _logger.LogInformation("Incoming {Method} {Path}", context.Request.Method, context.Request.Path);

                await _next(context);

                sw.Stop();
                _logger.LogInformation("Completed {Method} {Path} responded {StatusCode} in {Elapsed}ms",
                    context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.ElapsedMilliseconds);
            }
        }
    }
}
