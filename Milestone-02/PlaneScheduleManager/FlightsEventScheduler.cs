using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PlainScheduleController.Hubs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlaneScheduleManager
{
    internal class FlightsEventScheduler : IHostedService, IDisposable
    {
        private readonly IAudioManager _audioManager;
        private readonly IFlightsDataProcessor _flightsDataProcessor;
        private readonly IEventSelector _eventSelector;
        private readonly IHubContext<DevicesHub> _hubContext;

        public FlightsEventScheduler(
            IAudioManager audioManager,
            IFlightsDataProcessor flightsDataProcessor,
            IEventSelector eventSelector,
            IHubContext<DevicesHub> hubContext)
        {
            _audioManager = audioManager;
            _flightsDataProcessor = flightsDataProcessor;
            _eventSelector = eventSelector;
            _hubContext = hubContext;
        }

        public void Dispose()
        {
            
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var lastRunTime = DateTimeOffset.Now.AddMinutes(-1);

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var flight in _flightsDataProcessor.GetFlightData())
                {
                    var flightEvent = _eventSelector.GetFlightEvent(lastRunTime, flight.ArrivalTime, flight.DepartureTime);

                    if (flightEvent.HasValue)
                    {
                        var audioContent = _audioManager.GetAudio(flight, flightEvent.Value);
                        await _hubContext.Clients.All.SendAsync("ReceiveAudio", audioContent);
                    }
                }

                lastRunTime = DateTimeOffset.Now;

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
