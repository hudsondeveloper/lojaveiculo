using RabbitMQ.Client;

namespace LojaVeiculos.IRepository
{
    public interface IRabbitMq
    {
        ConnectionFactory conection();
    }
}