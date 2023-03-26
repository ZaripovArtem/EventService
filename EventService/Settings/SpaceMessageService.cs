using System.Text;
using RabbitMQ.Client;

namespace Features.Settings
{
    /// <summary>
    /// Продюссер для сервиса пространств
    /// </summary>
    public class SpaceMessageService : IMessageService
    {
        private readonly IModel _channel;

        /// <summary>
        /// Конструктор продюссера
        /// </summary>
        public SpaceMessageService()
        {
            var factory = new ConnectionFactory
            {
                // ReSharper disable once StringLiteralTypo имя Host'а
                HostName = "rabbitmq", Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            var conn = factory.CreateConnection();
            _channel = conn.CreateModel();
            _channel.QueueDeclare(queue: "SpaceDeleteEvent",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        /// <summary>
        /// Метод для публикации сообщения 
        /// </summary>
        /// <param name="messageString">сообщение</param>
        /// <returns></returns>
        public bool Enqueue(string messageString)
        {
            var body = Encoding.UTF8.GetBytes("server processed " + messageString);
            _channel.BasicPublish(exchange: "",
                routingKey: "SpaceDeleteEvent",
                basicProperties: null,
                body: body);
            Console.WriteLine("Опубликовано сообщение: {0} в RabbitMQ", messageString);
            return true;
        }
    }
}