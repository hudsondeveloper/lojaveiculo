
using ConsoleRabbit.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Timers;

namespace ConsoleRabbit
{

    class Program
    {
        private static System.Timers.Timer aTimer;
        private static ConnectionFactory factory;
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            IRabbit _rabbit = serviceProvider.GetService<IRabbit>();
            factory = _rabbit.conection();

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 20000;
            aTimer.Enabled = true;

            while (Console.Read() != 'x') ;
            aTimer.Stop();
            aTimer.Dispose();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRabbit, Rabbit>();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Aguardando evento: " + DateTime.Now);


            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "enviarEmailCadastroVeiculo", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);


                consumer.Received += (modelo, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Recebido {0}", message);
                    Email.SendMessage(message);

                    Console.WriteLine("[x] Concluído");
                };
                channel.BasicConsume(queue: "enviarEmailCadastroVeiculo", true, consumer: consumer);

            }
        }
        // private static void Consumer_Received(
        //      object sender, BasicDeliverEventArgs e)
        // {

        //     var body = e.Body.ToArray();
        //     var message = Encoding.UTF8.GetString(body);
        //     Console.WriteLine("Enviando Email");
        //     Email.SendMessage(message);
        // }
    }

}
