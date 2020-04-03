using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
 public  class ServiceBusClient
    {
        private readonly IQueueClient Client;
        public ServiceBusClient(string ServiceBusConnectionString, string QueueName)
        {
            Client = new QueueClient(ServiceBusConnectionString, QueueName);
        }

        public async Task<bool> SendMessagesAsync(string Message)
        {
            try
            {
                //Client = new QueueClient(ServiceBusConnectionString, QueueName);
                // Create a new message to send to the topic.
                var bytesMessage = new Message(Encoding.UTF8.GetBytes(Message));

                // Send the message to the topic.
                await Client.SendAsync(bytesMessage).ConfigureAwait(false);

                await Client.CloseAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception E)
            {
                Trace.WriteLine(E.Message);
                throw;
            }
        }
    }
}
