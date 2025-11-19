using System.Text;
using System.Text.Json;
using CartAPI.Services.Interface;
using RabbitMQ.Client;
using SharedBase.Dtos.RabbitMq;
using SharedBase.Models;

namespace CartAPI.Services
{
    public class RabbitMqMessageSender : IRabbitMqMessageSender, IDisposable
    {
      
        private IConnection _connection;
        private readonly IConfiguration _config;
        private IChannel _channel;

        public RabbitMqMessageSender(IConfiguration config)
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
            var body = GetMessageAsByteArray(message);
            var props = new BasicProperties();
            await _channel.BasicPublishAsync("", queueName, false, props, body);
            Console.WriteLine($"✅ Mensagem enviada para a fila {queueName}.");


        }
        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<CheckOutCartMsgDto>((CheckOutCartMsgDto)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
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
