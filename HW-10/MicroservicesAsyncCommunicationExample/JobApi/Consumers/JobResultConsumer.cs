using JobApi.Infra;
using MassTransit;
using QueueContracts;

namespace JobApi.Consumers;

public class JobResultConsumer(ITasksStorage storage, ILogger<JobResultConsumer> logger) : IConsumer<JobCompleted>
{
    public Task Consume(ConsumeContext<JobCompleted> context)
    {
        var message = context.Message;
        storage.MarkDone(message.Id, message.Result, message.CompletedAt);
        logger.LogInformation("Получен JobCompleted для {JobId}", message.Id);
        return Task.CompletedTask;
    }
}
