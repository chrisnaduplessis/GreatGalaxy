using FluentAssertions;
using GreatGalaxy.Administration.Requests.Driver;
using GreatGalaxy.Administration.Requests.Vehicle;
using GreatGalaxy.Administration.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace GreatGalaxy.Integration.Tests.Administration
{
    public class VehicleEndpointTests
    : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient client;

        public VehicleEndpointTests(TestWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task TestIt()
        {
            // Create a new vehicle
            var request = new CreateVehicleRequest("Interstaller Rover", "Model S", "Nothing fancy, but gets the job done", 500, 10, 30);

            var response = await client.PostAsJsonAsync(
                "/vehicles", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdVehicle = await response.Content
                .ReadFromJsonAsync<VehicleResponse>();

            AssertVehicleEquals(createdVehicle, request);
            createdVehicle.WhenScrapped.Should().BeNull();

            // Get the vehicle by id
            response = await client.GetAsync($"/vehicles/{createdVehicle.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var retreivedVehicle = await response.Content
                .ReadFromJsonAsync<VehicleResponse>();
            AssertVehicleEquals(retreivedVehicle, request);
            retreivedVehicle.WhenScrapped.Should().BeNull();

            // Scrap the vehicle
            response = await client.DeleteAsync($"/vehicles/{createdVehicle.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Get the vehicle again and check that it is scrapped
            response = await client.GetAsync($"/vehicles/{createdVehicle.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var scrappedVehicle = await response.Content
                .ReadFromJsonAsync<VehicleResponse>();
            AssertVehicleEquals(scrappedVehicle, request);
            scrappedVehicle.WhenScrapped.Should().NotBeNull();

            // Add another vehcicle
            var request2 = new CreateVehicleRequest("Starship enterprise", "Next Gen", "The big one!", 500, 10, 30);
            response = await client.PostAsJsonAsync(
                "/vehicles", request2);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdVehicle2 = await response.Content
                .ReadFromJsonAsync<VehicleResponse>();
            AssertVehicleEquals(createdVehicle2, request2);

            // Update the second vehicle
            var updateRequest = new UpdateVehicleRequest(createdVehicle2.Id, "Updated description");
            response = await client.PatchAsJsonAsync(
                $"/vehicles/{createdVehicle2.Id}", updateRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var updatedVehicle = await response.Content
                .ReadFromJsonAsync<VehicleResponse>();
            updatedVehicle.Should().NotBeNull();
            updatedVehicle.Id.Should().Be(createdVehicle2.Id);
            updatedVehicle.Description.Should().Be(updateRequest.Description);

            // Get all vehicles and check that both are there
            response = await client.GetAsync("/vehicles");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var vehicles = await response.Content
                .ReadFromJsonAsync<List<VehicleResponse>>();
            vehicles.Should().NotBeNull();
            vehicles.Should().ContainSingle(v => v.Id == createdVehicle.Id);
            vehicles.Should().ContainSingle(v => v.Id == createdVehicle2.Id);
        }

        private void AssertVehicleEquals(VehicleResponse expected, CreateVehicleRequest request)
        {
            expected.Should().NotBeNull();
            expected.Id.Should().BeGreaterThan(0);
            expected.Type.Should().NotBeNull();
            expected.Type.Make.Should().Be(request.Make);
            expected.Type.Model.Should().Be(request.Model);
            expected.Description.Should().Be(request.Description);
            expected.WeightAllowance.Should().Be(request.WeightAllowanceKg);
            expected.VolumeAllowance.Should().Be(request.VolumeAllowanceM3);
            expected.MaxSpeed.Should().Be(request.SpeedMetersPerSecond);
        }
    }
}
