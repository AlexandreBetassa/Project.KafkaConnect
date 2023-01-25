using Project.Domain.src.Contracts;
using System.Text.Json;

namespace Project.Domain.src
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IPublisherService _publisherSvc;
        private readonly IPublisherOptions _publisherOptions;

        public Services(IPublisherService kafkaSvc, IPublisherOptions publisherOptions)
        {
            _publisherSvc = kafkaSvc;
            _publisherOptions = publisherOptions;
        }

        public async Task Create(T entity)
        {
            string entityJson = JsonSerializer.Serialize(entity);
            await _publisherSvc.Send(_publisherOptions.Topic, null, entityJson);
        }
    }
}
