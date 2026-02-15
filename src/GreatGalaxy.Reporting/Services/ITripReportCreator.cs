using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Reporting.DomainModel;

namespace GreatGalaxy.Reporting.Services
{
    public interface ITripReportCreator
    {
        /// <summary>
        /// Retrieves the necessary data for the given delivery ID and constructs a TripReport object that encapsulates all relevant information about the trip,
        /// including route details, timestamps, driver and vehicle information, and any events that occurred during the delivery.
        /// </summary>
        /// <param name="deliveryId">DeliviryId</param>
        /// <returns>Trip Report</returns>
        TripReport CreteReport(DeliveryId deliveryId);
    }
}
