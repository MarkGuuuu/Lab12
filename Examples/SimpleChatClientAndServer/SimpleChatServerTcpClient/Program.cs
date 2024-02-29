using CS3500;
/// <summary>
///   Author: H. James de St. Germain
///   Date:   Spring 2022
///   
///   This code demonstrates a simple chat (broadcast) system using TcpClients.
/// </summary>


//
// Ask user for some startup information
//
Console.WriteLine( "Enter port to listen on (default 11000):" );
string? portStr = Console.ReadLine();
if (portStr == null)
{
    Console.WriteLine( "Exiting program." );
    return;
}

int port;

if ( !Int32.TryParse( portStr, out port ) )
{
    port = 11000;
}


//
// Start the server
//
new ChatServer( port ).StartServer();





