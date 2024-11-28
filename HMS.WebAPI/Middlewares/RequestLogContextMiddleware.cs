using Serilog;
using Serilog.Context;

namespace HMS.WebAPI.Middlewares
{
    public class RequestLogContextMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestLogContextMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            // Log Query Parameters
            var queryParams = context.Request.Query.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.ToString()
            );
            Log.Information("Query parameters: {@QueryParams}", queryParams);

            if (context.Request.HasFormContentType)
            {
                var formData = context.Request.Form.ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value.ToString()
                );
                Log.Information("Form data: {@FormData}", formData);
            }

            // Log Request Body
            if (context.Request.ContentLength > 0 &&
                                            context.Request.Body.CanRead &&
                                            context.Request.ContentType != null &&
                                            context.Request.ContentType.Contains("application/json"))
            {
                context.Request.Body.Position = 0;
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                Log.Information("Request: {Method} {Path} - Body: {Body}",
                    context.Request.Method, context.Request.Path, body);
                context.Request.Body.Position = 0; // Reset để controller đọc được
            }

            await _requestDelegate(context);
        }

        //public Task InvokeAsync(HttpContext context)
        //{
        //    using(LogContext.PushProperty("CorrelationId: ", context.TraceIdentifier))
        //    {
        //        return _requestDelegate(context);
        //    }
        //}
    }
}
