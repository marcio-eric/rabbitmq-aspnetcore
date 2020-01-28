using System;
using System.Text;
using RabbitMQ.Client;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "testes",
                Password = "Testes2018!"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "TestesASPNETCore",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Conteúdo da Mensagem: Menssagem";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: "TestesASPNETCore",
                basicProperties: null,
                body: body);
        }
    }
}