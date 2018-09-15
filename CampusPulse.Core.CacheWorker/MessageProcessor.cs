using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusPulse.Core.CacheWorker
{
   

    public class CacheProcessor<T>
    {
        private ICacheWriteManager cacheWriter;

        public CacheProcessor(ICacheWriteManager cacheWriter)
        {
            this.cacheWriter = cacheWriter;
        }
        public void process(T message)
        {
            //validate and translate message if needed
            //push to cache
            cacheWriter.
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
