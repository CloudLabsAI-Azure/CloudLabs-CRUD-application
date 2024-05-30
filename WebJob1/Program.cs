using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebJob1
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?linkid=2250384
    internal class Program
    {
        // Please set AzureWebJobsStorage connection strings in appsettings.json for this WebJob to run.
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices()
                    .AddAzureStorageQueues();
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Information);
                    b.AddConsole();
                });


            var host = builder.Build();
            using (host)
            {
                // The following code will invoke a function called CreateQueueMessage and
                // pass in data (value in this case) to the function
                var jobHost = host.Services.GetService(typeof(IJobHost)) as JobHost;
                var inputs = new Dictionary<string, object>
            {
                { "value", "Hello world!" }
            };

                await host.StartAsync();
                await jobHost.CallAsync("CreateQueueMessage", inputs);
                await host.StopAsync();
            }
        }
    }
}


