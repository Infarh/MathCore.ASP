using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Simple.OData.Client;

// ReSharper disable ArrangeMethodOrOperatorBody

namespace WebCore.ASP.ConsoleTests
{
    internal static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Waiting...");
            //Console.ReadLine();

            var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/students") };

            //var query = client.OData<Student>();
            //var selector = query.Select(s => s.FirstName);
            //var names = selector.ToArray();

            var odata = new ODataClient("http://localhost:5000/odata/");

            var students = await odata.For<Student>().Select(e => e.Course).ExecuteAsArrayAsync();

            foreach (var s in students)
                Console.WriteLine(s.FirstName);



            Console.ReadLine();
        }
    }
}
