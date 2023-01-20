using Microsoft.Extensions.DependencyInjection;

namespace Project.Kafka
{
    public static class Bootstrap
    {
        public static IServiceCollection ConnectServerKafka(this IServiceCollection services)
        {

            return services;
        }
    }
}
