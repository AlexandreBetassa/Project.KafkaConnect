﻿using Confluent.Kafka;
using Project.Domain.src.Contracts;

namespace Project.Infra.src.PubSub.Publisher
{
    public class KafkaService : IPublisherService
    {
        private readonly IPublisherOptions _publisherOptions;
        private IProducer<string, string>? Producer { get; set; }

        public KafkaService(IPublisherOptions publisherOptions)
        {
            _publisherOptions = publisherOptions;
        }

        public IProducer<string, string> GetClientPublisher()
        {
            var clientProducerConfig = new ProducerConfig
            {
                BootstrapServers = _publisherOptions.BootstrapServer,
                ApiVersionRequest = _publisherOptions.ApiVersionRequest,
                SaslUsername = _publisherOptions.Username,
                SaslPassword = _publisherOptions.Password,
                ClientId = _publisherOptions.ClientId,
                LingerMs = _publisherOptions.LingerMs,
                EnableDeliveryReports = _publisherOptions.EnableDeliveryReports,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                Acks = Acks.All,
                SaslMechanism = SaslMechanism.Plain,
            };

            var producer = new ProducerBuilder<string, string>(clientProducerConfig).Build();
            return producer;
        }

        public async Task Send(string topic, string key, string value)
        {
            try
            {
                Producer = GetClientPublisher();
                Producer.Produce(topic, new Message<string, string> { Key = key, Value = value });
                Producer.Flush();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
