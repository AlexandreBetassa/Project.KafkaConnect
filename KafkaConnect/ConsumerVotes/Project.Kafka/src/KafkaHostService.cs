using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Models.src.Contracts;

namespace Kafka.src
{
    public class KafkaHostService : BackgroundService
    {
        private IKafkaService _kafkaSvc;
        private ILogger<KafkaHostService> _logger;

        public KafkaHostService(ILogger<KafkaHostService> logger, IKafkaService kafkaSvc)
        {
            _logger = logger;
            _kafkaSvc = kafkaSvc;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => _kafkaSvc.ListenerKafka(stoppingToken), stoppingToken);
            _logger.LogInformation("KafkaService is run");
            return Task.CompletedTask;
        }
    }
}
