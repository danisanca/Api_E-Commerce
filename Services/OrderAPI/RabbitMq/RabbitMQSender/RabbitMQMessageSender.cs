using System.Text;
using System.Text.Json;
using OrderAPI.RabbitMq.RabbitMQSender.Interface;
using RabbitMQ.Client;
using SharedBase.Dtos.Cart;
using SharedBase.Dtos.RabbitMq;
using SharedBase.Models;

namespace OrderAPI.RabbitMq.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender, IDisposable
    {
        private IConnection _connection;
        private readonly IConfiguration _config;
        private IChannel _channel;

        public RabbitMQMessageSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task InitializeRabbitMq()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:hostname"],
                UserName = _config["RabbitMQ:username"],
                Password = _config["RabbitMQ:password"]
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            Console.WriteLine("✅ Conexão RabbitMQ inicializada no CartAPI");
        }


        public async Task SendMessage(BaseMessage message, string queueName)
        {
            if (!ConnectionExists())
            {
                Console.WriteLine("❌ Conexão RabbitMQ inexistente. Tentando reconectar...");
                await InitializeRabbitMq();
            }
            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var json = JsonSerializer.Serialize<PaymentMsgDto>((PaymentMsgDto)message);
            var body = Encoding.UTF8.GetBytes(json);
            var props = new BasicProperties();
            await _channel.BasicPublishAsync("", queueName, false, props, body);
            Console.WriteLine($"✅ Mensagem enviada para a fila '{queueName}'.");

        }
        private bool ConnectionExists()
        {
            return _connection != null && _connection.IsOpen;
        }

        public void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
        }
    }
}
