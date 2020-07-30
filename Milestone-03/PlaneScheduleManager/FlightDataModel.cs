using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneScheduleManager
{
    /// <summary>
    /// This class only contains a small subset of fields. In a real-life system, it would have a much larger set of fields
    /// </summary>
    internal class FlightDataModel
    {
        /// <summary>
        /// Real-life system would probably have a dictionary mapping international airport codes to the actual names.
        /// </summary>
        public string DestinationCode { get; set; }
        public string Gate { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public DateTimeOffset DepartureTime { get; set; }

    }
}
