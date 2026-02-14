using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Dispatch.Messages;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Dispatch.Services;
using MassTransit;

namespace GreatGalaxy.Dispatch.MessageConsumers
{
    public class DeliveryEventConsumer : IConsumer<DeliveryEventMessage>
    {
        private IDeliveryService deliveryService;

        public DeliveryEventConsumer(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }

        public async Task Consume(ConsumeContext<DeliveryEventMessage> context)
        {
            Console.WriteLine($"Received DispatchEvent: {context.Message} at {context.Message.Timestamp}");
            var message = context.Message;
            var deliveryEvent = new DeliveryEvent(message.Id, message.eventType, message.Timestamp, message.Duration, message.LocationId, message.Description);
            this.deliveryService.ProcessDeliveryEvent(context.Message.DeliveryId, deliveryEvent);
        }
    }
}
