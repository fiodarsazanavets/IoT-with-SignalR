using System;

namespace PlaneScheduleManager
{
    internal class EventSelector : IEventSelector
    {
        public FlightEvent? GetFlightEvent(DateTimeOffset lastRunTime, DateTimeOffset arrivalTime, DateTimeOffset departureTime)
        {
            var currentTime = DateTimeOffset.Now;

            if (currentTime > arrivalTime && lastRunTime < arrivalTime)
                return FlightEvent.Arrival;

            if (currentTime > departureTime.AddHours(-1) && lastRunTime < departureTime.AddHours(-1))
                return FlightEvent.GateOpen;

            if (currentTime > departureTime.AddMinutes(-30) && lastRunTime < departureTime.AddMinutes(-30))
                return FlightEvent.FinalCall;

            return null;
        }
    }
}
