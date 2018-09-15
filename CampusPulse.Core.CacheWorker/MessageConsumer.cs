using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusPulse.Core.CacheWorker
{
    public class CacheConsumer 
    {
        private ICacheWriteManager cacheManager;
        public CacheConsumer(ICacheWriteManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }
        public void StartListning()
        {
            var consumerConfig = new Dictionary<string, object>
            {
                { "group.id", "myconsumer" },
                { "bootstrap.servers", kafkaEndpoint },
            };

            // Create the consumer
            using (var consumer = new Consumer<Null, string>(consumerConfig, null, new StringDeserializer(Encoding.UTF8)))
            {
                // Subscribe to the OnMessage event
                consumer.OnMessage += (obj, msg) =>
                {
                    Console.WriteLine($"Received: {msg.Value}");
                };

                // Subscribe to the Kafka topic
                consumer.Subscribe(new List<string>() { kafkaTopic });

                // Handle Cancel Keypress 
                var cancelled = false;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };

                Console.WriteLine("Ctrl-C to exit.");

                // Poll for messages
                while (!cancelled)
                {
                    consumer.Poll();
                }
            }
        }
       
    }
}
