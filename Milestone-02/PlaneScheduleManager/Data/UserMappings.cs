﻿using System.Collections.Generic;

namespace PlaneScheduleManager.Data
{
    public static class UserMappings
    {
        private static Dictionary<string, string> connections = new Dictionary<string, string>();

        public static void AddDeviceConnected(string deviceId, string connectionId)
        {
            connections[connectionId] = deviceId;
        }

        public static void RemoveDeviceConnected(string connectionId)
        {
            connections.Remove(connectionId);
        }

        public static string GetDeviceId(string connectionId)
        {
            if (!connections.ContainsKey(connectionId))
                return string.Empty;

            return connections[connectionId];
        }
    }
}
