# Milestone 1 - Creating a basic SignalR setup

This solution consists of basic ASP.NET Core application with a SignalR hub and a .NET Core console application that communicates with the SignalR hub and sends regular heartbeat messages to it.

The server-side ASP.NET Core application also has a JavaScript SignalR client on its index page. It's purpose is to receive connection status from the .NET SignalR clients and update it on the page in real time.