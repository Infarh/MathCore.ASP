using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MathCore.ASP.WEB.Tests
{
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(host => host.UseStartup<Startup>())
                ;
    }
}
