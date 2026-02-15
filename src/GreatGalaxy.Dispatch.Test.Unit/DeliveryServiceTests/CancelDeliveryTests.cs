using FakeItEasy;
using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Dispatch.Models;
using Xunit;

namespace GreatGalaxy.Dispatch.Test.Unit.DeliveryServiceTests
{
    public class CancelDeliveryTests : BaseTest
    {
        [Fact]
        public void WhenDeliveryNotFoudThrowExeption()
        {
            // Arrange
            A.CallTo(() => this.FakeDeliveryRepository.Get(new DeliveryId(1))).Returns(null);

            // Act
            Assert.Throws<ArgumentException>(() => this.Subject.CancelDelivery(new DeliveryId(1)));
        }

        [Fact]
        public void WhenCalledCancleDelivery()
        {
            // Act
            this.Subject.CancelDelivery(new DeliveryId(1));

            // Assert
            A.CallTo(() => this.FakeDeliveryRepository.Update(A<Delivery>.That.Matches(_ => _.Status == DeliveryStatus.Cancelled))).MustHaveHappenedOnceExactly();
        }
    }
}
