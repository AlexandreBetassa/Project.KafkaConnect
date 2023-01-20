using Project.Contracts.src;
using System.Text.Json;

namespace Project.Services.src
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IKafkaService _kafkaSvc;
        private readonly IKafkaOptions _kafkaOptions;

        public Services(IKafkaService kafkaSvc, IKafkaOptions kafkaOptions)
        {
            _kafkaSvc = kafkaSvc;
            _kafkaOptions = kafkaOptions;
        }

        public async Task Create(T entity)
        {
            var entityJson = JsonSerializer.Serialize(entity);
            await Task.Run(() => _kafkaSvc.Send(_kafkaOptions.Topic, null, entityJson));
        }
    }
}
