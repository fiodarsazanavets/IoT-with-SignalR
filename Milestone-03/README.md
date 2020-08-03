# Milestone 3 - Enabling IoT applications to run as a single cluster

This solution adds the ability of the devices to communicate with each other via the SignalR hub.

Devices are mapped to areas, which are represented by SignalR groups, and individual departure gate locations, which are handled by a custom dictionary.

When the client application starts a playback, it sends a message to the hub, which broadcasts to all other application instances assigned to the same area that the playback has started. If any client application receives such message, it will postpone its own playback either until the other instance has completed playback or no such message was received within a specific timeout period.

Since the client app instances are now assigned to individual locations, the server application can now send audio data to individual devices, unless a specific location doesn't have an active client connection. If this is the case, it broadcasts data to all available devices in the area.