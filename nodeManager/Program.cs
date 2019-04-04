using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace nodeManager
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                Client.Init();
            }
            catch (Exception e)
            {
                Logger.Fail("Cannot Client.Init()");
            }

            var host = new WebHostBuilder()
            .UseKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10); })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseUrls("http://0.0.0.0:5000/")
            .Build();

            host.Run();

            ContainerLogic.Containers.ForEach(container => container.Remove());
            //CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
