using System;
using System.IO;

namespace PlaneScheduleManager
{
    /// <summary>
    /// This implementation obtains audio from a file, which it locates based on the destination airport code and event type.
    /// An alternative implementation may send a request to a text-to-speech service.
    /// </summary>
    internal class AudioFileManager : IAudioManager
    {
        private readonly string _audioPath;

        public AudioFileManager(string audioPath)
        {
            _audioPath = audioPath;
        }

        public byte[] GetAudio(FlightDataModel data, FlightEvent flightEvent)
        {
            var fullFileName = $"{_audioPath}{Path.DirectorySeparatorChar}{data.DestinationCode}_{flightEvent}.mp3";

            if (!File.Exists(fullFileName))
                return null;

            return File.ReadAllBytes(fullFileName);

            throw new NotImplementedException();
        }
    }
}
