using System.Text;
using System.Text.Json;
using CartAPI.Services.Interface;
using RabbitMQ.Client;
using SharedBase.Dtos.Cart;
using SharedBase.Models;

namespace CartAPI.Services
{
    public class RabbitMqMessageSender : IRabbitMqMessageSender
    {
      
        private IConnection _connection;
        private readonly IConfiguration _config;

        public RabbitMqMessageSender(IConfiguration config)
        {
            _config = config;
        }

        public async void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
               using var channel = await _connection.CreateChannelAsync();
               await channel.QueueDeclareAsync(queue: queueName, false, false, false, arguments: null);
               byte[] body = GetMessageAsByteArray(message);
               var props = new BasicProperties();
               await channel.BasicPublishAsync("",queueName, false, props, body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<CheckOutCartDto>((CheckOutCartDto)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private async void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _config.GetSection("RabbitMQ:hostname").Value,
                    UserName = _config.GetSection("RabbitMQ:username").Value,
                    Password = _config.GetSection("RabbitMQ:password").Value
                };
                _connection = await factory.CreateConnectionAsync();
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }
    }
}
