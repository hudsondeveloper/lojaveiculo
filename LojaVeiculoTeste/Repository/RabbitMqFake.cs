using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVeiculoTeste.Repository
{
    class RabbitMqFake : IRabbitMq
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
