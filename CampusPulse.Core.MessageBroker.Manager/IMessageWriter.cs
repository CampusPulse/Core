using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace CampusPulse.Core.Queue
{
    public interface IMessageWriter<T> where T : class
    {
        Task<Message<Null, string>> WriteAsync(T message, string topic);
        void Write(T message, string topic);

    }
}
