using LojaVeiculos.IRepository;
using RabbitMQ.Client;

namespace LojaVeiculos.RabbitMq
{
    public class RabbitMq : IRabbitMq
    {
        public ConnectionFactory conection()
        {
            ConnectionFactory factory;

            factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@rabbitmq:5672");
            factory.VirtualHost = "vhost";

            return factory;
        }
    }
}