using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WebJob1
{
    public class Functions
    {
        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will enqueue a message on an Azure Queue called outputqueue
        [NoAutomaticTrigger]
        public static void CreateQueueMessage([Queue("outputqueue")] out string message, string value, ILogger logger)
        {
            message = value;
            logger.LogInformation("Creating queue message: ", message);
        }
    }
}
