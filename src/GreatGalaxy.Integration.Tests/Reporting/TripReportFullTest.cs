using FluentAssertions;
using GreatGalaxy.Administration.Requests.Driver;
using GreatGalaxy.Administration.Requests.Vehicle;
using GreatGalaxy.Administration.Responses;
using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Dispatch.Messages;
using GreatGalaxy.Dispatch.Requests;
using GreatGalaxy.Dispatch.Responses;
using GreatGalaxy.Integration.Tests.Administration;
using GreatGalaxy.Integration.Tests.Dispatch;
using GreatGalaxy.Reporting.DomainModel;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace GreatGalaxy.Integration.Tests.Reporting
{
    public class TripReportFullTest
        : IClassFixture<AdministrationWebApplicationFactory>, IClassFixture<DispatchWebApplicationFactory>, IClassFixture<ReportingWebApplicationFactory>
    {
        private readonly HttpClient adminClient;
        private readonly HttpClient dispatchClient;
        private readonly HttpClient tripClient;
        private readonly ITestHarness harness;
        private readonly ITestOutputHelper testOutputHelper;

        public TripReportFullTest(
            AdministrationWebApplicationFactory administrationFactory,
            DispatchWebApplicationFactory dispatchFactory,
            ReportingWebApplicationFactory reportingFactory,
            ITestOutputHelper testOutputHelper)
        {
            this.adminClient = administrationFactory.CreateClient();
            this.dispatchClient = dispatchFactory.CreateClient();
            this.tripClient = reportingFactory.CreateClient();
            harness = dispatchFactory.Services.GetRequiredService<ITestHarness>();
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task DoIt()
        {
            var driverId = await this.CreateDriver();

            var vehicleId = await this.CreateVehicle();

            var route = await this.CreateRoute();

            // Create delivery
            var createResponse = await dispatchClient.PostAsJsonAsync("/deliveries", new
            {
                DriverId = driverId,
                VehicleId = vehicleId,
                RouteId = route.id,
            });
            createResponse.EnsureSuccessStatusCode();
            var deliviryResponse = await createResponse.Content.ReadFromJsonAsync<DeliveryResponse>();

            // Send departure event
            await this.PublishEvent(deliviryResponse.Id, EventType.VehicleDeparture, TimeSpan.FromSeconds(1), null, "And we are off on a great adventure!");

            // Arrive at first checkpoint
            await this.PublishEvent(deliviryResponse.Id, EventType.CheckpointReached, TimeSpan.FromSeconds(1), route.checkpoints[0].LocationId, "Got to the first checkpoint with no issues");

            // Disaster strikes!
            await this.PublishEvent(deliviryResponse.Id, EventType.Disaster, TimeSpan.FromHours(10), null, "Space priates! They got half of the cargo, but we are back on route.");

            // Arrive at next checkpoint
            await this.PublishEvent(deliviryResponse.Id, EventType.CheckpointReached, TimeSpan.FromDays(10), route.checkpoints[1].LocationId, "Finally reached the moon, will need to spend some time on repairs");

            // Finish Trip
            await this.PublishEvent(deliviryResponse.Id, EventType.DestinationReached, TimeSpan.FromSeconds(1), null, "Mission successfull, well sort of...");

            // Make sure all messages were consumed
            await harness.Sent.SelectAsync<DeliveryEventMessage>().Take(5).ToListAsync();


            // Generate report
            var response = await this.tripClient.GetAsync(
                $"/trips/{deliviryResponse.Id}");
            response.EnsureSuccessStatusCode();
            var tripReport = await response.Content
                .ReadFromJsonAsync<TripReport>();

            this.testOutputHelper.WriteLine(JsonSerializer.Serialize(
                tripReport,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }

        private async Task<int> CreateDriver()
        {
            // Create a new driver
            var request = new CreateDriverRequest("Zyrrik Fluxrunner");

            var response = await this.adminClient.PostAsJsonAsync(
                "/drivers", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var driver = await response.Content
                .ReadFromJsonAsync<DriverResponse>();

            return driver.Id;
        }

        private async Task<int> CreateVehicle()
        {
            // Create a new vehicle
            var request = new CreateVehicleRequest("Nebuluxe Interstellar", "StarScoot HyperHauler 3000", "Top of the fleet", 500, 10, 30);

            var response = await adminClient.PostAsJsonAsync(
                "/vehicles", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdVehicle = await response.Content
                .ReadFromJsonAsync<VehicleResponse>();

            return createdVehicle.Id;
        }

        private async Task<DeliveryRouteResponse> CreateRoute()
        {
            var createRouteRequest = new CreateRouteRequest
            (
                "123 Main St",
                "Apt 4B",
                "Zork",
                40.7128,
                -74.0060,
                "Mars",
                "0,0,0",
                new List<AddCheckpointRequest>
                {
                     new (
                        "Martian imigration center",
                        "",
                        "",
                        39.9526,
                        -75.1652,
                        "Phobos",
                        "0,0,0"),
                     new (
                        "Way Station 1",
                        "",
                        "",
                        39.9526,
                        -75.1652,
                        "Moon",
                        "0,0,0")
                },
                "Level/260 Oteha Valley Road",
                "Albany",
                "Auckland 0632",
                -36.722418671875594,
                174.70709234000125,
                "Earth",
                "0,0,0"
            );


            var response = await dispatchClient.PostAsJsonAsync("/routes", createRouteRequest);
            response.EnsureSuccessStatusCode();
            var routeResponse = await response.Content.ReadFromJsonAsync<DeliveryRouteResponse>();

            return routeResponse;
        }

        private async Task PublishEvent(int deliveryId, EventType eventType, TimeSpan timespan, int? checkpointId, string description)
        {
            //** Depart the delivery
            // Add an event to the delivery
            var addEventResponse = await dispatchClient.PostAsJsonAsync($"/deliveries/{deliveryId}/events",
                new AddDeliveryEventRequest(deliveryId, eventType, DateTime.UtcNow, TimeSpan.FromSeconds(1), checkpointId, description)
            );
            addEventResponse.EnsureSuccessStatusCode();

            // Verify that the event was published and consumed
            Assert.True(await harness.Published.Any<DeliveryEventMessage>());

            Assert.True(await harness.Consumed.Any<DeliveryEventMessage>());
        }
    }
}
