﻿using CS3500;

/// <summary>
///   Author: H. James de St. Germain
///   Date:   Spring 2022
///
///   This code demonstrates a simple chat (broadcast) system using TcpClients.
/// </summary>

//
// Ask user for some startup information
//
Console.WriteLine( "Enter the host to connect to (default localhost):" );
string? hostStr = Console.ReadLine();
string host = "localhost";
if ( hostStr == null )
{
    Console.WriteLine( "Exiting program." );
    return;
}
else if ( hostStr != "" )
{
    host = hostStr;
}

Console.WriteLine( "Enter port to connect to (default 11000):" );
string? portStr = Console.ReadLine();
if ( portStr == null )
{
    Console.WriteLine( "Exiting program." );
    return;
}

int port;

if ( !Int32.TryParse( portStr, out port ) )
{
    port = 11000;
}

// Start the client listening for messages
new ChatClient( host, port ).Listen();

// Let Program sit waiting for messages from server
Console.Read();