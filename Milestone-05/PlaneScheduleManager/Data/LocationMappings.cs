using System.Collections.Generic;

namespace PlaneScheduleManager.Data
{
    public static class LocationMappings
    {
        private static readonly Dictionary<string, string> gateMappings = new Dictionary<string, string>();

        public static void MapDeviceToGate(string gateNumber, string connectionId)
        {
            gateMappings[gateNumber] = connectionId;
        }

        public static string GetConnectionId(string gateNumber)
        {
            if (!gateMappings.ContainsKey(gateNumber))
                return string.Empty;

            return gateMappings[gateNumber];
        }
    }
}
