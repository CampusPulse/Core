using CampusPulse.Core.Domain;
using CampusPulse.Core.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CampusPulse.Core.MessageBroker.Manager.Test
{
    [TestClass]
    public class MessageWriterTest
    {
        [TestMethod]
        public void WriteAsyncTest()
        {
            var config = new Dictionary<string, object>
            {
            { "bootstrap.servers", "192.168.0.104:9092" }
            };
            IMessageWriter<Book> writer = new KafkaWriter<Book>(config);
            Book book = new Book() { Acedamics = "ymdl", Author = "atul" };
            var res = writer.WriteAsync(book, "topic-book").Result;
        }
    }
}
