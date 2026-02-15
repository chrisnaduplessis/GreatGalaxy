using FakeItEasy;
using GreatGalaxy.Dispatch.Repositories;
using GreatGalaxy.Dispatch.Services;

namespace GreatGalaxy.Dispatch.Test.Unit.DeliveryServiceTests
{
    public class BaseTest
    {
        public IDeliveryRepository FakeDeliveryRepository { get; } = A.Fake<IDeliveryRepository>();

        public DeliveryService Subject { get; set;  }

        public BaseTest()
        {
            this.Subject = new DeliveryService(this.FakeDeliveryRepository);
        }
    }
}
