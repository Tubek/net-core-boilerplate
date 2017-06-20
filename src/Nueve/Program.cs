using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Nueve
{
    /// <summary>
    /// App startup
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
