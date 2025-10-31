using SharedBase.Models;

namespace CartAPI.Services.Interface
{
    public interface IRabbitMqMessageSender
    {
        Task SendMessage(BaseMessage baseMessage, string queueName);
        public Task InitializeRabbitMq();
    }
}
