using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace MathCore.ASP.Middleware
{
    public class ProcessingTimeHeaderMiddleware
    {
        private const string __HeaderName = "Request-Processing-time";

        private readonly RequestDelegate _Next;

        public ProcessingTimeHeaderMiddleware(RequestDelegate Next) => _Next = Next;

        public async Task Invoke(HttpContext Context)
        {
            var timer = Stopwatch.StartNew();

            Context.Response.OnStarting(() =>
                {
                    Context.Response.Headers.Add(__HeaderName, $"{timer.Elapsed.TotalMilliseconds:0.##} ms");
                    return Task.CompletedTask;
                });
           
            await _Next(Context);
            
            
        }
    }
}
