# Milestone 2 - Enabling real-time audio transfer via SignalR

This solution builds upon the solution from the previos milestone.

The server-side application now has a scheduler that reads flight data. It then checks the timings agains the hard-coded business rules and, if it's due to trigger an audio event, it selects an MP3 file, reads its content and sends it to connected devices via SignalR.

The client device plays audio upon receiving the data. It uses [NetCoreAudio](https://github.com/mobiletechtracker/NetCoreAudio) NuGet package, which is already designed to play audio on either Windows or Linux.