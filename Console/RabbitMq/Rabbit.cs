using RabbitMQ.Client;

namespace ConsoleRabbit.RabbitMq
{
    public class Rabbit : IRabbit
    {
        public ConnectionFactory conection()
        {
            ConnectionFactory factory;

            factory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "vhost",
                UserName = "guest",
                Password = "guest"
            };

            return factory;
        }
    }
}