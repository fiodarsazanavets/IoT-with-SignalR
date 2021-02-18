# Milestone 3 - Group IoT applications by area

This solution groups the IoT devices by area, so only those devices that are located in a paticular area will receive audio.

The areas that the devices are mapped to are represented by SignalR groups, which are handled by a custom dictionary.

Individual gates are mapped to areas. So, when an announcable even happens at a particular gate, only the devices in that area are notified.