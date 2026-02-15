using FluentAssertions;
using GreatGalaxy.Administration.Models;
using GreatGalaxy.Administration.Requests.Driver;
using GreatGalaxy.Administration.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace GreatGalaxy.Integration.Tests.Administration
{
    public class DriverEndpointTests
    : IClassFixture<AdministrationWebApplicationFactory>
    {
        private readonly HttpClient client;

        public DriverEndpointTests(AdministrationWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task TestIt()
        {
            // Create a new driver
            var request = new CreateDriverRequest ("Test Driver");

            var response = await client.PostAsJsonAsync(
                "/drivers", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var driver = await response.Content
                .ReadFromJsonAsync<DriverResponse>();

            AssertDriverResponse(driver, new DriverResponse
            (
                0,
                request.Name,
                null,
                new HashSet<int>()
             ));


            // Get the driver by ID
            response = await client.GetAsync($"/drivers/{driver.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getResponse = await response.Content
                .ReadFromJsonAsync<DriverResponse>();
            
            var expectedResponse = new DriverResponse
            (
                driver.Id,
                request.Name,
                null,
                new HashSet<int>()
             );
            AssertDriverResponse(getResponse, expectedResponse);

            // Update the driver
            var updateRequest = new RenameDriverRequest("Updated Driver");
            response = await client.PatchAsJsonAsync(
                $"/drivers/{driver.Id}", updateRequest);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get the driver again to verify updates
            response = await client.GetAsync($"/drivers/{driver.Id}");
            expectedResponse = new DriverResponse
            (
                driver.Id,
                updateRequest.Name,
                null,
                new HashSet<int>()
             );
            getResponse = await response.Content
                .ReadFromJsonAsync<DriverResponse>();

            AssertDriverResponse(getResponse, expectedResponse);

            // retire the driver
            response = await client.PostAsJsonAsync(
               $"/drivers/{driver.Id}/retire", new { });
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get the driver again to verify updates
            response = await client.GetAsync($"/drivers/{driver.Id}");
            expectedResponse = new DriverResponse
            (
                driver.Id,
                updateRequest.Name,
                DateTime.UtcNow, // Retired should be set to a value close to now
                new HashSet<int>()
             );
            getResponse = await response.Content
                .ReadFromJsonAsync<DriverResponse>();
            AssertDriverResponse(getResponse, expectedResponse);
            // Reactivate the driver
            response = await client.PostAsJsonAsync(
               $"/drivers/{driver.Id}/reactivate", new { });
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get the driver again to verify updates
            response = await client.GetAsync($"/drivers/{driver.Id}");
            expectedResponse = new DriverResponse
            (
                driver.Id,
                updateRequest.Name,
                null,
                new HashSet<int>()
             );
            getResponse = await response.Content
                .ReadFromJsonAsync<DriverResponse>();
            AssertDriverResponse(getResponse, expectedResponse);

            // approve vehicle for the driver
            response = await client.PostAsJsonAsync(
               $"/drivers/{driver.Id}/approved-vehicles/", new ApproveVehicleRequest(1));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Get the driver again to verify updates
            response = await client.GetAsync($"/drivers/{driver.Id}");
            expectedResponse = new DriverResponse
            (
                driver.Id,
                updateRequest.Name,
                null,
                new List<int> { 1 }
             );
            getResponse = await response.Content
                .ReadFromJsonAsync<DriverResponse>();
            AssertDriverResponse(getResponse, expectedResponse);

            //delete approval for vehicle
            response = await client.DeleteAsync(
               $"/drivers/{driver.Id}/approved-vehicles/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Get the driver again to verify updates
            response = await client.GetAsync($"/drivers/{driver.Id}");
            expectedResponse = new DriverResponse
            (
                driver.Id,
                updateRequest.Name,
                null,
                new List<int>()
             );
            getResponse = await response.Content
                .ReadFromJsonAsync<DriverResponse>();
            AssertDriverResponse(getResponse, expectedResponse);
        }

        private static void AssertDriverResponse(DriverResponse response, DriverResponse expectedResponse)
        {
            response.Should().NotBeNull();
            response.Id.Should().BeGreaterThan(0);
            response.Name.Should().Be(expectedResponse.Name);
            if (expectedResponse.Retired == null)
            {
                response.Retired.Should().BeNull();
            }
            else
            {
                response.Retired.Should().BeCloseTo(expectedResponse.Retired.Value, TimeSpan.FromDays(1));
            }
            if (expectedResponse.ApprovedVehicleIds == null)
            {
                response.ApprovedVehicleIds.Should().BeNull();
            }
            else
            {
                response.ApprovedVehicleIds.Should().BeEquivalentTo(expectedResponse.ApprovedVehicleIds);
            }
        }
    }
}
