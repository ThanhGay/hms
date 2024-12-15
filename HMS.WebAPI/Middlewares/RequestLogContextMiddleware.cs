using System.Text.Json;
using Serilog;
using Microsoft.AspNetCore.Http;

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

            // Log Form Data
            if (context.Request.HasFormContentType)
            {
                var formData = context.Request.Form.ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value.ToString()
                );

                // Lọc thông tin nhạy cảm
                formData = SanitizeSensitiveData(formData);

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

                try
                {
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
                    if (jsonData != null)
                    {
                        // Lọc thông tin nhạy cảm
                        jsonData = SanitizeSensitiveData(jsonData);
                        body = JsonSerializer.Serialize(jsonData); // Serialize lại sau khi lọc
                    }
                }
                catch
                {
                    // Nếu body không phải JSON hợp lệ, bỏ qua bước lọc
                    Log.Warning("Không phải JSON hợp lệ");
                }

                Log.Information("Request: {Method} {Path} - Body: {Body}",
                    context.Request.Method, context.Request.Path, body);

                context.Request.Body.Position = 0; // Reset để controller đọc được
            }

            await _requestDelegate(context);
        }

        private static Dictionary<string, string> SanitizeSensitiveData(Dictionary<string, string> data)
        {
            var sensitiveKeys = new[] { "password", "otp" };
            return data.ToDictionary(
                kvp => kvp.Key,
                kvp => sensitiveKeys.Contains(kvp.Key.ToLower()) ? "*****" : kvp.Value
            );
        }
    }
}
