using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Models.src.Contracts;

namespace Kafka.src
{
    public class SubscribeHostService : BackgroundService
    {
        private ISubscribeService _subscribe;
        private ILogger<SubscribeHostService> _logger;

        public SubscribeHostService(ILogger<SubscribeHostService> logger, ISubscribeService subscribe)
        {
            _logger = logger;
            _subscribe = subscribe;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => _subscribe.ListenerServer(stoppingToken), stoppingToken);
            _logger.LogInformation("Subscribe Service is run");
            return Task.CompletedTask;
        }
    }
}
