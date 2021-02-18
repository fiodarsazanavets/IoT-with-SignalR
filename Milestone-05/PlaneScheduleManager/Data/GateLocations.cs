using System.Collections.Generic;


namespace PlaneScheduleManager.Data
{
    internal static class GateLocations
    {
        private static readonly Dictionary<string, string> gateLoactions = new Dictionary<string, string>
        {
            {"1", "North Wing"},
            {"2", "North Wing"},
            {"3", "South Wing"},
            {"4", "South Wing"}
        };

        public static string GetLocationName(string gateNumber)
        {
            if (gateLoactions.ContainsKey(gateNumber))
                return gateLoactions[gateNumber];

            return string.Empty;
        }
    }
}
