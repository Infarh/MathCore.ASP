using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MathCore.ASP.HostedServices
{
    /// <summary>
    /// <code>services.AddHostedService&lt;HostedService&gt;();</code>
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2&amp;tabs=visual-studio"/>
    /// </remarks>
    // https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2&tabs=visual-studio
    public abstract class HostedService :IHostedService, IDisposable
    {
        public abstract Task StartAsync(CancellationToken Cancel);

        public abstract Task StopAsync(CancellationToken Cancel);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }
    }
}
