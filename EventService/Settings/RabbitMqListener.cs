using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Features.Settings;

/// <summary>
/// Класс консьюмера
/// </summary>
public class RabbitMqListener : BackgroundService
{
    private readonly IConnection? _conn;
    private readonly IModel _channel;

    /// <summary>
    /// Конструктор консьюмера
    /// </summary>
    public RabbitMqListener()
    {

        var factory = new ConnectionFactory
        {
            // ReSharper disable once StringLiteralTypo имя Host'а
            HostName = "rabbitmq",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        do
        {
            try
            {
                _conn = factory.CreateConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(1000);
                _conn = null;
            }
        } while (_conn == null);

        _channel = _conn.CreateModel();
        _channel.QueueDeclare(queue: "EventDeleteEvent",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    /// <summary>
    /// Обработка получения сообщений с очереди
    /// </summary>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        // ReSharper disable once UnusedParameter.Local
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            Console.WriteLine($"Получено сообщение: {content}");

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("EventDeleteEvent", false, consumer);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Очистка мусора
    /// </summary>
    public override void Dispose()
    {
        _channel.Close();
        _conn?.Close();
        base.Dispose();
    }
}
