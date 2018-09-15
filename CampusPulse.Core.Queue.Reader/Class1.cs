using CampusPulse.Core.Domain;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampusPulse.Core.Streaming 
{
    public class Class1
    {
        public void Test()
        {
            var bookAggregates = new Dictionary<string, Book>();
            var config = new Dictionary<string, Object>();
            config.Add("bootstrap.servers", "clusterino:667");
            config.Add("group.id", "book-consumer");
            config.Add("enable.auto.commit", true);
            config.Add("auto.commit.interval.ms", 5000);
            config.Add("batch.num.messages", 50000);
            config.Add("queue.buffering.max.ms", 300);

            var topicConfig = new Dictionary<string, Object>();
            topicConfig.Add("auto.offset.reset", "earliest");
            config.Add("default.config.topic", topicConfig);
            var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8));
            consumer.OnMessage += (sender, e) =>
            {
                throw new NotImplementedException();
            };
        }
        
    }
}
