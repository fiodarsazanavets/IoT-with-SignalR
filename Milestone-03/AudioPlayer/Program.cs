using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Program
    {
        private static readonly int timeoutSeconds = 60;

        static async Task Main()
        {
            Console.WriteLine("Please provide device identifier.");
            var identifier = Console.ReadLine();

            Console.WriteLine("Please provide the area name for the device.");
            var areaName = Console.ReadLine();

            Console.WriteLine("Please provide departure gate number for the device to be positioned at.");
            var gateNumber = Console.ReadLine();

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:57100/devicesHub")
                .Build();

            connection.On<byte[]>("ReceiveAudio", HandleFileData);

            await connection.StartAsync();
            await connection.InvokeAsync("ReceiveDeviceConnected", identifier, areaName, gateNumber);

            while (true)
            {
                await connection.InvokeAsync("ReceiveHeartbeat", identifier);
                await Task.Delay(30000);
            }

            async Task HandleFileData(byte[] content)
            {
                FileManager.CreateFile(content);
                Console.WriteLine("Playback Started");
                await AudioManager.PlayAudio();
                Console.WriteLine("Playback Finished");
            }
        }
    }
}
