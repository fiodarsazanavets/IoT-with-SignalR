using System;

namespace PlaneScheduleManager
{
    internal interface IEventSelector
    {
        FlightEvent? GetFlightEvent(DateTimeOffset lastRunTime, DateTimeOffset arrivalTime, DateTimeOffset departureTime);
    }
}
