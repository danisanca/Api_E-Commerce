using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using OrderAPI.Models;
using OrderAPI.RabbitMq.RabbitMQSender.Interface;
using OrderAPI.Repository;
using OrderAPI.Repository.Interface;
using OrderAPI.Services.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedBase.Constants.RabbitMQ;
using SharedBase.Dtos.Cart;
using SharedBase.Dtos.RabbitMq;

namespace OrderAPI.RabbitMq.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private IConnection _connection;
        private readonly IConfiguration _config;
        private IChannel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(IServiceScopeFactory scopeFactory,
            IRabbitMQMessageSender rabbitMQMessageSender, IConfiguration config)
        {
            _config = config;
            _scopeFactory = scopeFactory;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await CreateConnectionWithRetry();
                await CreateChannelWithRetry();

                if (!ConnectionExists())
                {
                    Console.WriteLine("❌ Falha ao estabelecer conexão com RabbitMQ.");
                    return;
                }

                // Garante que a fila exista antes do consumo
                await _channel!.QueueDeclareAsync(
                    queue: ConfigRabbitMq.checkOutQueue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                Console.WriteLine("✅ Conexão com RabbitMQ estabelecida. Aguardando mensagens...");

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.ReceivedAsync += async (model, evt) =>
                {
                    try
                    {
                        var body = evt.Body.ToArray();
                        var content = Encoding.UTF8.GetString(body);

                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Console.WriteLine("⚠️ Mensagem vazia recebida. Ignorando.");
                            await _channel.BasicAckAsync(evt.DeliveryTag, false);
                            return;
                        }

                        Console.WriteLine($"📩 Mensagem recebida: {content}");

                        var vo = JsonSerializer.Deserialize<CheckOutCartMsgDto>(content);
                        if (vo == null)
                        {
                            Console.WriteLine("⚠️ Mensagem deserializada nula. Ignorando.");
                            await _channel.BasicAckAsync(evt.DeliveryTag, false);
                            return;
                        }
                        if (vo != null)
                            await ProcessOrder(vo);

                        Console.WriteLine("✅ Pedido processado com sucesso.");

                        await _channel.BasicAckAsync(evt.DeliveryTag, false);
                        Console.WriteLine("🟢 ACK enviado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro ao processar mensagem: {ex}");
                        try
                        {
                            // Reencaminha mensagem para a fila (requeue = true)
                            await _channel!.BasicNackAsync(evt.DeliveryTag, false, true);
                        }
                        catch (Exception nackEx)
                        {
                            Console.WriteLine($"⚠️ Erro ao enviar NACK: {nackEx.Message}");
                        }
                    }
                };

                await _channel.BasicConsumeAsync(
                    queue: ConfigRabbitMq.checkOutQueue,
                    autoAck: false,
                    consumer: consumer);

                // Mantém o serviço ativo
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro no consumidor RabbitMQ: {ex}");
            }
        }
        private async Task ProcessOrder(CheckOutCartMsgDto message)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderServices>();
                var existingOrder = await orderService.GetHeaderById(message.Id);
                if (existingOrder != null)
                {
                    Console.WriteLine($"⚠️ Pedido duplicado detectado (ID: {message.Id}). Ignorando processamento.");
                    return; // Ignora e envia ACK normalmente no método chamador
                }

                OrderHeader order = new()
                    {
                        Id = message.Id,
                        UserId = message.UserId,
                        ListCount = message.ListCount,
                        PaymentStatus = false,
                        OrderDetails = new List<OrderDetail>()

                    };
                    foreach (var details in message.CartDetail)
                    {
                        OrderDetail detail = new OrderDetail()
                        {
                            OrderHeaderId = order.Id,
                            ProductId = details.ProductId,
                            ProductName = details.ProductName,
                            Price = details.Price,
                            Discount = details.Discount,
                            Count = details.Count,
                        };
                        order.TotalPrice += (detail.Price - detail.Discount);
                        order.TotalDiscont += detail.Discount;
                        order.OrderDetails.Add(detail);
                    }
                    await orderService.Create(order);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao processar pedido: {ex.Message}");
                throw;
            }
        }
        private async Task CreateConnectionWithRetry()
        {
            int attempts = 0;
            const int maxAttempts = 5;

            while (attempts < maxAttempts)
            {
                try
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = _config["RabbitMQ:hostname"],
                        UserName = _config["RabbitMQ:username"],
                        Password = _config["RabbitMQ:password"]
                    };

                    _connection = await factory.CreateConnectionAsync();
                    Console.WriteLine("🔌 Conexão RabbitMQ criada com sucesso.");
                    return;
                }
                catch (Exception ex)
                {
                    attempts++;
                    Console.WriteLine($"⚠️ Tentativa {attempts}/{maxAttempts} - Falha ao conectar ao RabbitMQ: {ex.Message}");
                    await Task.Delay(2000);
                }
            }

            throw new Exception("❌ Não foi possível criar conexão RabbitMQ após várias tentativas.");
        }
        private async Task CreateChannelWithRetry()
        {
            int attempts = 0;
            const int maxAttempts = 5;

            while (attempts < maxAttempts)
            {
                try
                {
                    if (_connection == null)
                        throw new InvalidOperationException("A conexão RabbitMQ não foi inicializada.");

                    _channel = await _connection.CreateChannelAsync();

                    await _channel.QueueDeclareAsync(
                        queue: ConfigRabbitMq.checkOutQueue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    Console.WriteLine("📡 Canal e fila inicializados.");
                    return;
                }
                catch (Exception ex)
                {
                    attempts++;
                    Console.WriteLine($"⚠️ Tentativa {attempts}/{maxAttempts} - Falha ao criar canal: {ex.Message}");
                    await Task.Delay(2000);
                }
            }

            throw new Exception("❌ Não foi possível criar canal RabbitMQ após várias tentativas.");
        }

        private bool ConnectionExists()
        {
            return _connection != null && _connection.IsOpen;
        }
    }
}
