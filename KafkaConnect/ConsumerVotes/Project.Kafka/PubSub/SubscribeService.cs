using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Project.Models.src.Contracts;
using Project.Models.src.Entities;
using System.Text.Json;

namespace Kafka.src
{
    public class SubscribeService : ISubscribeService
    {
        private IServices<Vote> _svc;
        private readonly IKafkaOptions _kafkaOptions;
        private ILogger<SubscribeService> _logger;

        public SubscribeService(IServices<Vote> svc, IKafkaOptions kafkaOptions, ILogger<SubscribeService> logger)
        {
            _svc = svc;
            _kafkaOptions = kafkaOptions;
            _logger = logger;
        }

        protected IConsumer<string, string> GetClientKafka()
        {
            var clientConsumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaOptions.BootstrapServer,
                AutoOffsetReset = _kafkaOptions.AutoOffsetResetApp,
                ApiVersionRequest = _kafkaOptions.ApiVersionRequest,
                EnableAutoCommit = _kafkaOptions.AutoCommit,
                ClientId = _kafkaOptions.ClientId,
                GroupId = _kafkaOptions.GroupId,
            };
            var consumer = new ConsumerBuilder<string, string>(clientConsumerConfig).Build();
            return consumer;
        }

        public async Task ListenerServer(CancellationToken cancellation)
        {
            var consumer = GetClientKafka();
            consumer.Subscribe(_kafkaOptions.Topic);
            while (!cancellation.IsCancellationRequested)
            {
                var message = consumer.Consume(_kafkaOptions.TimeConsume);
                _logger.LogInformation("Listening " + _kafkaOptions.Topic);
                if (message != null)
                {
                    var vote = JsonSerializer.Deserialize<Vote>(message.Message.Value);
                    if (_svc.Create(vote!).IsCompletedSuccessfully)
                    {
                        _logger.LogInformation("Vote computed.");
                        consumer.Commit(message);
                    }
                    else
                        _logger.LogInformation("Vote not computed.");
                }
            }
        }
    }
}
