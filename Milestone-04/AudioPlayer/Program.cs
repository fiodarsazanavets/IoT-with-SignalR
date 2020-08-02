using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Program
    {
        private static bool holdOffPlayback = false;
        private static readonly int timeoutSeconds = 60;

        static async Task Main()
        {
            var identifier = Environment.GetEnvironmentVariable("DEVICE_IDENTIFIER");
            var areaName = Environment.GetEnvironmentVariable("AREA_NAME");
            var gateNumber = Environment.GetEnvironmentVariable("GATE_NUMBER");
            var hubUrl = Environment.GetEnvironmentVariable("HUB_URL");

            var connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            connection.On<byte[]>("ReceiveAudio", HandleFileData);
            connection.On<bool>("ReceivePlaybackStatus", (playing) => holdOffPlayback = playing);

            await connection.StartAsync();
            Console.WriteLine("Connection Established");
            await connection.InvokeAsync("ReceiveDeviceConnected", identifier, areaName, gateNumber);
            Console.WriteLine("Device Registered");

            while (true)
            {
                await connection.InvokeAsync("ReceiveHeartbeat", identifier);
                await Task.Delay(30000);
            }

            async Task HandleFileData(byte[] content)
            {
                var receiveTime = DateTimeOffset.Now;

                while (holdOffPlayback)
                {
                    Console.WriteLine("Other device is playing audio. Waiting...");
                    if (DateTimeOffset.Now.AddSeconds(-timeoutSeconds) > receiveTime)
                        holdOffPlayback = false;

                    await Task.Delay(1000);
                }

                FileManager.CreateFile(content);
                await connection.InvokeAsync("BroadcastPlaybackStatus", areaName, true);
                Console.WriteLine("Playback Started");
                await AudioManager.PlayAudio();
                Console.WriteLine("Playback Finished");
                await connection.InvokeAsync("BroadcastPlaybackStatus", areaName, false);
            }
        }
    }
}
