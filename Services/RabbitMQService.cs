namespace FloodAlertAPI.Services;

using RabbitMQ.Client;
using System.Text;

public class RabbitMQService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName = "flood-alert-queue";

    public RabbitMQService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest", // ou seu usuário
            Password = "guest"  // ou sua senha
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "",
                              routingKey: _queueName,
                              basicProperties: null,
                              body: body);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
