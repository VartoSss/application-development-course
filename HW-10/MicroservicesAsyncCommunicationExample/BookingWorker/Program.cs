using BookingWorker.Consumers;
using MassTransit;
using QueueContracts;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<JobTaskConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("RabbitMQ");
        var host = rabbit.GetValue("Host", "localhost")!;
        var port = rabbit.GetValue("Port", 5672);
        var vhost = rabbit.GetValue("VirtualHost", "/")!;

        cfg.Host(host, (ushort)port, vhost, h =>
        {
            h.Username(rabbit.GetValue("Username", "guest")!);
            h.Password(rabbit.GetValue("Password", "guest")!);
        });

        cfg.ReceiveEndpoint(QueueNames.Tasks, e =>
        {
            e.PrefetchCount = 1;
            e.ConcurrentMessageLimit = 1;
            e.ConfigureConsumer<JobTaskConsumer>(context);
        });
    });
});

await builder.Build().RunAsync();
