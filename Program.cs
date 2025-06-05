using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using FloodAlertAPI.Data;
using FloodAlertAPI.Services;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("FloodDB"));

builder.Services.AddSingleton<RabbitMQService>();
builder.Services.AddSingleton<PredictService>();

// Rate Limit
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "alertaEnchente", durable: false, exclusive: false, autoDelete: false, arguments: null);
Console.WriteLine(" [*] Aguardando alertas...");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] ALERTA RECEBIDO: {message}");
};

channel.BasicConsume(queue: "alertaEnchente", autoAck: true, consumer: consumer);
Console.ReadLine();

app.UseIpRateLimiting();
app.UseAuthorization();
app.MapControllers();
app.Run();
