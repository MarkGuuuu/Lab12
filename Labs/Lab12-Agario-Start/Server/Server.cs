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