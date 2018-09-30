using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace CampusPulse.Core.Queue
{
    public class KafkaWriter<T> : IMessageWriter<T> where T : class
    {
        private readonly Dictionary<string, object> config;
      
        //private readonly ILogger logger;
        public KafkaWriter(Dictionary<string, object> config)
        {
            this.config = config;
        }
        public void Write(T message, string topic)
        {
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var dr = producer.ProduceAsync(topic, null, JsonConvert.SerializeObject(message)).Result;
                Task.Run(() => Log.Information($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}"));
            }

        }

        public async Task<Message<Null, string>> WriteAsync(T message, string topic)
        {
            //var config = new Dictionary<string, object>
            //{
            //    { "bootstrap.servers", "localhost:9092" }
            //};

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                return await producer.ProduceAsync(topic, null, JsonConvert.SerializeObject(message));
                //Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }

        }
    }
}
