using RabbitMQ.Client;

namespace ConsoleRabbit.RabbitMq
{
    public interface IRabbit
    {
        ConnectionFactory conection();
    }
}