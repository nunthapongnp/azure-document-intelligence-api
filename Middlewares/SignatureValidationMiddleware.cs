using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using AzureDocumentIntelligence.Models;

namespace AzureDocumentIntelligence.Middlewares
{
    public class SignatureValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;

        public SignatureValidationMiddleware(RequestDelegate next, IOptions<SecuritySettings> securitySettings)
        {
            _next = next;
            _secretKey = securitySettings.Value.SecretKey;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            if (!request.Headers.TryGetValue("X-Timestamp", out var timestamp) ||
                !request.Headers.TryGetValue("X-Signature", out var signature))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Missing security headers.");
                return;
            }

            if (!long.TryParse(timestamp, out var requestTime) || Math.Abs(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - requestTime) > 300)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid or expired timestamp.");
                return;
            }

            if (!_secretKey.Equals(signature))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid signature.");
                return;
            }

            await _next(context);
        }
    }
}
