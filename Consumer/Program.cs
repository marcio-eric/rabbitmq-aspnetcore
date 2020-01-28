using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "TestesASPNETCore",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine("Aguardando mensagens para processamento");
            Console.WriteLine("Pressione uma tecla para encerrar...");
            Console.ReadKey();
        }
        
        private static void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body);
            Console.WriteLine(Environment.NewLine +
                              "[Nova mensagem recebida] " + message);
        }
    }
}