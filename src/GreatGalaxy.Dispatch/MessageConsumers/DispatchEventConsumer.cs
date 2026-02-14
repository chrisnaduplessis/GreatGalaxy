using GreatGalaxy.Dispatch.Messages;
using MassTransit;

namespace GreatGalaxy.Dispatch.MessageConsumers
{
    public class DispatchEventConsumer : IConsumer<DispatchEvent>
    {
        public async Task Consume(ConsumeContext<DispatchEvent> context)
        {
            Console.WriteLine($"Received DispatchEvent: {context.Message.Name} at {context.Message.Timestamp}");
        }
    }
}
