using SharedBase.Models;

namespace OrderAPI.RabbitMq.RabbitMQSender.Interface
{
    public interface IRabbitMQMessageSender
    {
        Task SendMessage(BaseMessage message, string queueName);
        public Task InitializeRabbitMq();
    }
}
