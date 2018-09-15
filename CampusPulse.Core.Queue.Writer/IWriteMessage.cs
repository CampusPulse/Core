using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace CampusPulse.Core.Queue
{
    public interface IWriteMessage<T> where T : class
    {
        Task<Message<Null, string>> PutAsync(T message, string topic);
        void Put(T message, string topic);

    }
}
