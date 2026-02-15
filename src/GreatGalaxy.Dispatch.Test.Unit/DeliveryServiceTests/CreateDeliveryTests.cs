using FakeItEasy;
using FluentAssertions;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Dispatch.Models;
using Xunit;

namespace GreatGalaxy.Dispatch.Test.Unit.DeliveryServiceTests
{
    public class CreateDeliveryTests : BaseTest
    {
        [Fact]
        public void WhenCalledSetUpDelivery()
        {
            // Act
            var result = this.Subject.CreateDelivery(new DriverId(1), new VehicleId(2), new RouteId(3));

            // Assert
            A.CallTo(() => this.FakeDeliveryRepository.Create(A<Delivery>._)).MustHaveHappened();
        }
    }
}
