using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampusPulse.Core.Queue
{
    public class WriteMessage<T> : IWriteMessage<T> where T : class
    {
        private readonly Dictionary<string, object> config;
        private readonly string topic;
        public WriteMessage(Dictionary<string, object> config , string topic)
        {
            this.config = config;
            this.topic = topic;

        }
        public void Put(T message, string topic)
        {
            //var config = new Dictionary<string, object>
            //{
            //    { "bootstrap.servers", "localhost:9092" }
            //};

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var dr = producer.ProduceAsync(topic, null, JsonConvert.SerializeObject(message)).Result;
                Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }

        }

        public async Task<Message<Null, string>> PutAsync(T message, string topic)
        {
            //var config = new Dictionary<string, object>
            //{
            //    { "bootstrap.servers", "localhost:9092" }
            //};

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                return await  producer.ProduceAsync(topic, null, JsonConvert.SerializeObject(message));
                //Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }

        }
    }
}
