using JobApi.Consumers;
using JobApi.Infra;
using MassTransit;
using Microsoft.OpenApi;
using QueueContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITasksStorage, TaskStorage>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Job API",
        Version = "v1",
        Description = "Приём задач на бронирование. Обработка идёт через очередь RabbitMQ."
    });
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<JobResultConsumer>();

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

        cfg.ReceiveEndpoint(QueueNames.Results, e =>
        {
            e.ConfigureConsumer<JobResultConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Job API v1");
});

app.MapControllers();
app.Run();
