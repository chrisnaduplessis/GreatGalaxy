using FluentAssertions;
using GreatGalaxy.Dispatch.Requests;
using GreatGalaxy.Dispatch.Responses;
using System.Net.Http.Json;
using Xunit;

namespace GreatGalaxy.Integration.Tests.Dispatch
{
    public class RoutesEndpointTests
        : IClassFixture<DispatchWebApplicationFactory>
    {
        private readonly HttpClient client;

        public RoutesEndpointTests(DispatchWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task TestIt()
        {
            //Create a route request with all fields populated, including checkpoints
            var createRouteRequest = new CreateRouteRequest
            (
                "123 Main St",
                "Apt 4B",
                "New York, NY 10001",
                40.7128,
                -74.0060,
                "Earth",
                "0,0,0",
                new List<AddCheckpointRequest>
                {
                    new (
                        "456",
                        "Elm St",
                        "Philadelphia, PA 19103",
                        39.9526,
                        -75.1652,
                        "Earth",
                        "0,0,0")
                },
                "789 Oak St",
                "-",
                "Washington, DC 20001",
                38.9072,
                -77.0369,
                "Earth",
                "0,0,0"
            );
            var response = await client.PostAsJsonAsync("/routes", createRouteRequest);
            response.EnsureSuccessStatusCode();
            var routeResponse = await response.Content.ReadFromJsonAsync<DeliveryRouteResponse>();
            AssertRouteResponse(routeResponse, createRouteRequest);

            // Get the route by ID and assert the response
            var getResponse = await client.GetAsync($"/routes/{routeResponse.id}");
            getResponse.EnsureSuccessStatusCode();
            var getRouteResponse = await getResponse.Content.ReadFromJsonAsync<DeliveryRouteResponse>();
            AssertRouteResponse(getRouteResponse, createRouteRequest);

            // Add checkpoints to the existing route
            var addCheckpointRequest = new AddCheckpointRequest
            (
                "101",
                "Pine St",
                "Baltimore, MD 21201",
                39.2904,
                -76.6122,
                "Earth",
                "0,0,0"
            );
            var addCheckpointResponse = await client.PostAsJsonAsync($"/routes/{routeResponse.id}/checkpoints", addCheckpointRequest);
            addCheckpointResponse.EnsureSuccessStatusCode();
        }

        public static void AssertRouteResponse(DeliveryRouteResponse actual, CreateRouteRequest expected)
        {
            actual.Should().NotBeNull();
            actual.origen.Line1.Should().Be(expected.OriginLine1);
            actual.origen.Line2.Should().Be(expected.OriginLine2);
            actual.origen.Line3.Should().Be(expected.OriginLine3);
            actual.origen.Latitude.Should().Be(expected.OriginLatitude);
            actual.origen.Longitude.Should().Be(expected.OriginLongitude);
            actual.origen.CelestialBody.Should().Be(expected.OriginCelestialBody);
            actual.origen.CelestialBodyPosition.Should().Be(expected.OriginCelestialBodyPosition);
            actual.checkpoints.Should().HaveCount(expected.Checkpoints.Count);
            for (int i = 0; i < expected.Checkpoints.Count; i++)
            {
                var expectedCheckpoint = expected.Checkpoints[i];
                var actualCheckpoint = actual.checkpoints[i];
                actualCheckpoint.Line1.Should().Be(expectedCheckpoint.Line1);
                actualCheckpoint.Line2.Should().Be(expectedCheckpoint.Line2);
                actualCheckpoint.Line3.Should().Be(expectedCheckpoint.Line3);
                actualCheckpoint.Latitude.Should().Be(expectedCheckpoint.Latitude);
                actualCheckpoint.Longitude.Should().Be(expectedCheckpoint.Longitude);
                actualCheckpoint.CelestialBody.Should().Be(expectedCheckpoint.CelestialBody);
                actualCheckpoint.CelestialBodyPosition.Should().Be(expectedCheckpoint.CelestialBodyPosition);
            }
            actual.destination.Line1.Should().Be(expected.DestinationLine1);
            actual.destination.Line2.Should().Be(expected.DestinationLine2);
            actual.destination.Line3.Should().Be(expected.DestinationLine3);
            actual.destination.Latitude.Should().Be(expected.DestinationLatitude);
            actual.destination.Longitude.Should().Be(expected.DestinationLongitude);
            actual.destination.CelestialBody.Should().Be(expected.DestinationCelestialBody);
            actual.destination.CelestialBodyPosition.Should().Be(expected.DestinationCelestialBodyPosition);
        }
    }
}
