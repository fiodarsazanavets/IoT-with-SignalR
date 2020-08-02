using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PlaneScheduleManager.Data;
using PlaneScheduleManager.Hubs;
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

        private readonly string receiveAudioMethodName = "ReceiveAudio";

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

                        if (flightEvent.Value == FlightEvent.Arrival)
                        {
                            await _hubContext.Clients.All.SendAsync(receiveAudioMethodName, audioContent);
                        }
                        else
                        {
                            var connectionId = LocationMappings.GetConnectionId(flight.Gate);

                            if (!string.IsNullOrWhiteSpace(connectionId))
                                await _hubContext.Clients.Client(connectionId).SendAsync(receiveAudioMethodName, audioContent);
                            else
                                await _hubContext.Clients.Groups(GateLocations.GetLocationName(flight.Gate)).SendAsync(receiveAudioMethodName, audioContent);
                        }
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
