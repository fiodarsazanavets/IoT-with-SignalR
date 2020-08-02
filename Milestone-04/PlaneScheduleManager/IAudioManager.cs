namespace PlaneScheduleManager
{
    internal interface IAudioManager
    {
        byte[] GetAudio(FlightDataModel data, FlightEvent flightEvent);
        byte[] GetAudio(string fileName);
    }
}
