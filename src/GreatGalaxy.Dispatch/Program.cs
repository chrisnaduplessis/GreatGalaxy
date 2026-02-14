using GreatGalaxy.Dispatch.MessageConsumers;
using GreatGalaxy.Dispatch.Messages;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(_ =>
{
    _.AddConsumer<DispatchEventConsumer>();

    _.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var publishEndpoint = scope.ServiceProvider
        .GetRequiredService<IPublishEndpoint>();

    await publishEndpoint.Publish(new DispatchEvent(Guid.NewGuid(), "Test Event", "This is a test dispatch event", 1, 1, 1, DateTime.UtcNow));

    Console.WriteLine("Test message published");
});



app.UseSwagger();
app.UseSwaggerUI();

app.Run();