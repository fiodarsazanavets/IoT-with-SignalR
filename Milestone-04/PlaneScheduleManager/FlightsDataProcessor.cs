using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlaneScheduleManager
{
    internal class FlightsDataProcessor : IFlightsDataProcessor
    {
        private readonly string _inputDataPath; 

        public FlightsDataProcessor(string inputDataPath)
        {
            _inputDataPath = inputDataPath;
        }

        public List<FlightDataModel> GetFlightData()
        {
            var flights = new List<FlightDataModel>();

            string fileText = File.ReadAllText(_inputDataPath + Path.DirectorySeparatorChar + Constants.InputFileName);
            
            if (string.IsNullOrWhiteSpace(fileText))
                return flights;
            
            var dataLines = fileText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 1; i < dataLines.Length; i++)
            {
                var columns = dataLines[i].Split(',');
                var baseDate = DateTimeOffset.Now.Date;

                flights.Add(new FlightDataModel
                {
                    DestinationCode = columns[17],
                    Gate = columns[^1],
                    ArrivalTime = baseDate + GetTimeFromString(columns[4]),
                    DepartureTime = baseDate + GetTimeFromString(columns[6])
                });
            }

            return flights;
        }

        private TimeSpan GetTimeFromString(string input)
        {
            if (input.Length < 4)
                input = $"0{input}";

            return new TimeSpan(int.Parse(input.Substring(0, 2)), int.Parse(input.Substring(2, 2)), 0);
        }
    }
}
