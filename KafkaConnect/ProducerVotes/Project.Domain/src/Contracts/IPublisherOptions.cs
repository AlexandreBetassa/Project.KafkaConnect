﻿using Confluent.Kafka;

namespace Project.Domain.src.Contracts
{
    public interface IPublisherOptions
    {
        public string Topic { get; set; }
        public string BootstrapServer { get; set; }
        public string ClientId { get; set; }
        public bool ApiVersionRequest { get; set; }
        public int LingerMs { get; set; }
        public bool EnableDeliveryReports { get; set; }
        public Acks AcksApp { get; }
        public SecurityProtocol Protocol { get; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
