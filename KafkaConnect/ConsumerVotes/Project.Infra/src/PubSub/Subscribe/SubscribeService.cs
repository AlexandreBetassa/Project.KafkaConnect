using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Project.Domain.src.Contracts;
using Project.Domain.src.Entities;
using System.Text.Json;

namespace Project.Infra.src.PubSub.Subscribe
{
    public class SubscribeService : ISubscribeService
    {
        private IServices<Vote> _svc;
        private readonly ISubscribeOptions _subscribeOptions;
        private ILogger<SubscribeService> _logger;

        public SubscribeService(IServices<Vote> svc, ISubscribeOptions subscribeOptions, ILogger<SubscribeService> logger)
        {
            _svc = svc;
            _subscribeOptions = subscribeOptions;
            _logger = logger;
        }

        protected IConsumer<string, string> GetClientSubscribe()
        {
            var clientConsumerConfig = new ConsumerConfig
            {
                BootstrapServers = _subscribeOptions.BootstrapServer,
                AutoOffsetReset = _subscribeOptions.AutoOffsetResetApp,
                ApiVersionRequest = _subscribeOptions.ApiVersionRequest,
                EnableAutoCommit = _subscribeOptions.AutoCommit,
                ClientId = _subscribeOptions.ClientId,
                GroupId = _subscribeOptions.GroupId,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslPassword = _subscribeOptions.Password,
                SaslUsername = _subscribeOptions.Username
            };
            var consumer = new ConsumerBuilder<string, string>(clientConsumerConfig).Build();
            return consumer;
        }

        public async Task ListenerServer(CancellationToken cancellation)
        {
            var consumer = GetClientSubscribe();
            consumer.Subscribe(_subscribeOptions.Topic);
            while (!cancellation.IsCancellationRequested)
            {
                var message = consumer.Consume(_subscribeOptions.TimeConsume);
                _logger.LogInformation("Listening " + _subscribeOptions.Topic);
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
