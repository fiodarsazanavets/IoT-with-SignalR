using Microsoft.AspNetCore.SignalR;
using PlaneScheduleManager.Data;
using System;
using System.Threading.Tasks;

namespace PlaneScheduleManager.Hubs
{
    public class DevicesHub : Hub
    {
        public async Task ReceiveHeartbeat(string deviceId)
        {
            await Clients.Groups("Master").SendAsync("UpdateDeviceStatus", deviceId, DateTimeOffset.UtcNow);
        }

        public async Task ReceiveDeviceConnected(string deviceId, string areaName, string gateNumber)
        {
            UserMappings.AddDeviceConnected(deviceId, Context.ConnectionId);           
            LocationMappings.MapDeviceToGate(gateNumber, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, areaName);
            await Clients.Groups("Master").SendAsync("ChangeConnectionStatus", deviceId, true);
        }

        public async Task SendAudioToDevices(byte[] content)
        {
            await Clients.All.SendAsync("ReceiveAudio", content);
        }

        public async Task RegisterAsManager()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Master");
        }

        public async Task BroadcastPlaybackStatus(string areaName, bool playing)
        {
            await Clients.Groups(areaName).SendAsync("ReceiveDeviceConnected", playing);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var deviceId = UserMappings.GetDeviceId(Context.ConnectionId);
            await Clients.Groups("Master").SendAsync("ChangeConnectionStatus", deviceId, false);
            UserMappings.RemoveDeviceConnected(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
