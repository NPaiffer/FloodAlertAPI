using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System.Text;

namespace FloodAlertAPI.Services;

public class RabbitMQService
{
        private readonly RabbitMQ.Client.IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;

    public RabbitMQService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "flood_alert_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void PublicarMensagem(string mensagem)
    {
        var body = Encoding.UTF8.GetBytes(mensagem);

        _channel.BasicPublish(exchange: "",
                              routingKey: "flood_alert_queue",
                              basicProperties: null,
                              body: body);

        Console.WriteLine($" [✔️] Mensagem enviada: {mensagem}");
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
