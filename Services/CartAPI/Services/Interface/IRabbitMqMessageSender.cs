using SharedBase.Models;

namespace CartAPI.Services.Interface
{
    public interface IRabbitMqMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
