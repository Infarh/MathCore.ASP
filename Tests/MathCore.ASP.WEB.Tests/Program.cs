using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MathCore.ASP.WEB.Tests
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateWebHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(host =>
                {
                    host.UseStartup<Startup>();
                });
    }
}
