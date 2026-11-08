using System.Collections.Concurrent;
using MassTransit;
using QueueContracts;

namespace BookingWorker.Consumers;

public class JobTaskConsumer(
    ILogger<JobTaskConsumer> logger,
    ISendEndpointProvider sendEndpointProvider,
    IConfiguration configuration) : IConsumer<JobTaskMessage>
{
    private static readonly ConcurrentDictionary<Guid, byte> Processed = new();

    public async Task Consume(ConsumeContext<JobTaskMessage> context)
    {
        var message = context.Message;

        // At-least-once: брокер может доставить то же сообщение ещё раз.
        // Пока процесс жив, повтор с тем же id не выполняем.
        if (Processed.ContainsKey(message.Id))
        {
            logger.LogInformation("Повторная доставка задачи {JobId}, пропускаю", message.Id);
            return;
        }

        var delaySeconds = configuration.GetValue("Processing:DelaySeconds", 3);
        logger.LogInformation("Начата обработка задачи {JobId}", message.Id);
        await Task.Delay(TimeSpan.FromSeconds(delaySeconds), context.CancellationToken);

        var completed = new JobCompleted
        {
            Id = message.Id,
            Result = $"Комната {message.RoomId} забронирована с {message.StartTime:O} по {message.EndTime:O}",
            CompletedAt = DateTime.UtcNow
        };

        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.Results}"));
        await endpoint.Send(completed, context.CancellationToken);
        Processed.TryAdd(message.Id, 0);

        logger.LogInformation("Задача {JobId} выполнена, результат отправлен в очередь {Queue}", message.Id, QueueNames.Results);
    }
}
