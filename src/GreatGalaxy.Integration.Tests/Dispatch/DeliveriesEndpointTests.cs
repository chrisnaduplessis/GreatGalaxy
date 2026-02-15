using FluentAssertions;
using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Dispatch.Messages;
using GreatGalaxy.Dispatch.Requests;
using GreatGalaxy.Dispatch.Responses;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace GreatGalaxy.Integration.Tests.Dispatch
{
    public class DeliveriesEndpointTests
        : IClassFixture<DispatchWebApplicationFactory>
    {
        private readonly HttpClient client;
        private readonly ITestHarness harness;

        public DeliveriesEndpointTests(DispatchWebApplicationFactory factory)
        {
            client = factory.CreateClient();
            harness = factory.Services.GetRequiredService<ITestHarness>();
        }

        [Fact]
        public async Task TestIt()
        {
            // Start the test harness for MassTransit
            await harness.Start();

            // Create a delivery
            var createResponse = await client.PostAsJsonAsync("/deliveries", new
            {
                DriverId = 1,
                VehicleId = 2,
                RouteId = 3
            });
            createResponse.EnsureSuccessStatusCode();
            var deliviryResponse = await createResponse.Content.ReadFromJsonAsync<DeliveryResponse>();
            deliviryResponse.Should().NotBeNull();
            deliviryResponse.DriverId.Should().Be(1);
            deliviryResponse.VehicleId.Should().Be(2);
            deliviryResponse.RouteId.Should().Be(3);

            // Get the delivery
            var getResponse = await client.GetAsync($"/deliveries/{deliviryResponse.Id}");
            getResponse.EnsureSuccessStatusCode();
            var getDeliveryResponse = await getResponse.Content.ReadFromJsonAsync<DeliveryResponse>();
            getDeliveryResponse.Should().NotBeNull();
            getDeliveryResponse.Should().BeEquivalentTo(deliviryResponse);


            //** Depart the delivery
            // Add an event to the delivery
            var addEventResponse = await client.PostAsJsonAsync($"/deliveries/{deliviryResponse.Id}/events", 
                new AddDeliveryEventRequest(deliviryResponse.Id, Common.ValueTypes.Event.EventType.VehicleDeparture, DateTime.UtcNow, TimeSpan.FromSeconds(1), null, "We're off!")
            );
            addEventResponse.EnsureSuccessStatusCode();

            // Verify that the event was published and consumed
            Assert.True(await harness.Published.Any<DeliveryEventMessage>());

            Assert.True(await harness.Consumed.Any<DeliveryEventMessage>());

            // Get the delivery again and verify that the event was added and processed correctly
            var getResponse2 = await client.GetAsync($"/deliveries/{deliviryResponse.Id}");
            getResponse2.EnsureSuccessStatusCode();
            var getDeliveryResponse2 = await getResponse2.Content.ReadFromJsonAsync<DeliveryResponse>();
            getDeliveryResponse2.Should().NotBeNull();
            getDeliveryResponse2.Events.Should().HaveCount(1);
            getDeliveryResponse2.Events[0].EventType.Should().Be(Common.ValueTypes.Event.EventType.VehicleDeparture);
            getDeliveryResponse2.Events[0].Description.Should().Be("We're off!");
            getDeliveryResponse2.Status.Should().Be(DeliveryStatus.InTransit);
            getDeliveryResponse2.Departed.Should().NotBeNull();

            ///** Arrive the delivery
            addEventResponse = await client.PostAsJsonAsync($"/deliveries/{deliviryResponse.Id}/events",
                new AddDeliveryEventRequest(deliviryResponse.Id, Common.ValueTypes.Event.EventType.DestinationReached, DateTime.UtcNow, TimeSpan.FromSeconds(1), null, "Home at last!")
            );
            addEventResponse.EnsureSuccessStatusCode();

            // Verify that the 2nd event was consumed
            var consumed = await harness.Consumed
                .SelectAsync<DeliveryEventMessage>()
                .Take(2)
                .ToListAsync();

            // Get the delivery again and verify that the event was added and processed correctly
            var getResponse3 = await client.GetAsync($"/deliveries/{deliviryResponse.Id}");
            getResponse3.EnsureSuccessStatusCode();

            var getDeliveryResponse3 = await getResponse3.Content.ReadFromJsonAsync<DeliveryResponse>();
            getDeliveryResponse3.Should().NotBeNull();
            getDeliveryResponse3.Events.Should().HaveCount(2);
            getDeliveryResponse3.Events[1].EventType.Should().Be(Common.ValueTypes.Event.EventType.DestinationReached);
            getDeliveryResponse3.Events[1].Description.Should().Be("Home at last!");
            getDeliveryResponse3.Status.Should().Be(DeliveryStatus.Delivered);
            getDeliveryResponse3.Arrived.Should().NotBeNull();

            await harness.Stop();
        }
    }
}
