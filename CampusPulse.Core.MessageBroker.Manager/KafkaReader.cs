using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampusPulse.Core.MessageBroker.Manager
{
    public class KafkaReader
    {
        private readonly Dictionary<string, object> config;
        public KafkaReader(Dictionary<string, object> config)
        {
            this.config = config;
        }
        public void StartListning(string topic)
        {


            // Create the consumer
            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                // Subscribe to the OnMessage event
                consumer.OnMessage += (obj, msg) =>
                {
                    Console.WriteLine($"Received: {msg.Value}");
                };

                // Subscribe to the Kafka topic
                consumer.Subscribe(new List<string>() { topic });

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
