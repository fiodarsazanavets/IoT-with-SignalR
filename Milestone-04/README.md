# Milestone 4 - Enabling IoT appication deployment via Docker

This solution has an example of a Dockerfile for building a Linux Docker container image for the .NET client application. It also has an example of docker-compose.yml file that has all parameters pre-configured to run this application on a Linux device.

The content of the docker-compose.yml file is equivalent of the following "docker run" command:

```
docker run --name audioplayer --device /dev/snd -v /usr/bin/aplay -e DEVICE_IDENTIFIER=12345 -e AREA_NAME="North Wing" -e GATE_NUMBER=1 -e HUB_URL=http://localhost:57100/devicesHub fsazanavets/signalr-audio-player:linux
```