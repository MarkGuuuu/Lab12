// See https://aka.ms/new-console-template for more information

// CS 3500
// Lab 12
// Add networking code here following lab instructions.
//
// Here is the sequence of events that should happen
// 
// Reminder: networking actions can occur on separate threads.
// 
// Protocol:
//     Server                              Client
//     -------------------------------------
//     Waits for Connections
//                                         Tries to Connect
//
//     onConnect                           onContact method called (by networking code)
//       don't do anything                    .
//                                            .
//                                            Sends "Phase1"
//                                            awaits response
//     onMessage
//       expects "Phase1"
//       sends "Phase1-Agreed"
//       awaits more data
//
//                                          onMessage called (by networking code)
//                                             message must be Phase1-Agreed
//                                             Sends "Send Boxes"
//                                             awaits more data
//     onMessage
//       expects "Send Boxes"
//       sends a random number of boxes
//        one after the other
//
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a Box
//                                          onMessage
//                                            receives a BoxConsole.WriteLine( "CS 3500 - Jims Sample - Lab 12 Code!" );

using System.Text.Json;
using Communications;
using Lab12Models;
using Microsoft.Extensions.Logging;

// let's use a logger in this project for communication and debugging
var loggerFactory = LoggerFactory.Create( builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation( "Server code is coming during the lab!" );

logger.LogInformation("Server code is coming during the lab!");
logger.LogDebug("CS 3500 - Jim Sample - Lab 12 Code!");
bool phase2 = false;

var clientAwaiter = new Networking(logger, ClientConnected, ClientDisconnected, HandleIncomingClientMessage);

async void HandleIncomingClientMessage(Networking channel, string message)
{
    if (message == "Phase1")
    {
        await channel.SendAsync("Phase1-Agreed");
        logger.LogDebug("Phase1-Agreed!");
        phase2 = true;
    }
    else if (message == "Send Boxes" && phase2)
    {
        Console.Write("Sending the Boxes!\n");
        for (int i = 0; i < new Random().Next(10) + 1; i++)
        {
            var box = new Box();
            var boxString = JsonSerializer.Serialize(box);

            if (i == 0)
            {
                logger.LogDebug($"About to send the following box: {boxString}");
            }

            await channel.SendAsync(boxString);
        }
    }
    else
    {
        logger.LogDebug("You messed up the protocol!");
    }
}

void ClientDisconnected(Networking channel)
{
    //throw new NotImplementedException();
}

void ClientConnected(Networking channel)
{
    //throw new NotImplementedException();
}

await clientAwaiter.WaitForClientsAsync(11000, true);

logger.LogDebug("Press Enter to End Server");
Console.Read();

