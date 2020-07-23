using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Please provide device identifier.");
            var identifier = Console.ReadLine();

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:57100/devicesHub")
                .Build();

            connection.On<byte[]>("ReceiveAudio", HandleFileData);

            await connection.StartAsync();
            await connection.InvokeAsync("ReceiveDeviceConnected", identifier);

            while (true)
            {
                await connection.InvokeAsync("ReceiveHeartbeat", identifier);
                await Task.Delay(30000);
            }

            static async Task HandleFileData(byte[] content)
            {
                FileManager.CreateFile(content);
                await AudioManager.PlayAudio();
            }
        }
    }
}
