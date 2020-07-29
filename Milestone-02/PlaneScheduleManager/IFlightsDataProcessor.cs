using System.Collections.Generic;

namespace PlaneScheduleManager
{
    internal interface IFlightsDataProcessor
    {
        List<FlightDataModel> GetFlightData();
    }
}
