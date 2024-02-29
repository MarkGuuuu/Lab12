using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CS3500
{

    /// <summary>
    ///   Author: H. James de St. Germain
    ///   Date:   Spring 2022
    ///   
    ///   A simple server for sending (broadcasting) simple text messages 
    ///   to multiple clients.
    ///   
    ///   Demonstrates:
    ///     1) Threads
    ///     2) Locks
    ///     3) Async/Await Usage
    /// </summary>
    class ChatServer
    {
        /// <summary>
        /// keep track of how big a message to send... keep getting bigger!
        /// </summary>
        private long message_length = 5000;


        /// <summary>
        /// A list of all clients currently connected
        /// </summary>
        private List<TcpClient> clients = new();

        /// <summary>
        /// The central connection point.
        /// </summary>
        private TcpListener network_listener;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="port"> the computer port to listen on</param>
        public ChatServer( int port = 11000 )
        {
            network_listener = new TcpListener( IPAddress.Any, port );
        }

        /// <summary>
        /// Start accepting Tcp socket connections from clients
        /// </summary>
        public void StartServer()
        {

            Console.WriteLine( $"Server waiting for clients here: {network_listener.LocalEndpoint}" );

            network_listener.Start();

            new Thread( WaitForClients ).Start();

            // waits for the (server) user to type messages, then sends them
            SendMessageUILoop();
        }

        /// <summary>
        /// Handle client connections.  
        /// 
        /// Warning: Race Condition between this thread and any use of clients field.
        /// </summary>
        private async void WaitForClients( )
        {
            while (true)
            {
                TcpClient connection = await network_listener.AcceptTcpClientAsync();
                Console.WriteLine( $"\n ** New Connection ** Accepted From {connection.Client.RemoteEndPoint} to {connection.Client.LocalEndPoint}\n" );
                lock ( this.clients )
                {
                    this.clients.Add( connection );
                }
            }
        }

        /// <summary>
        /// Continuously:
        ///  1) ask the user for a message to send to the client
        ///  2) encode the message as a UTF8 byte array
        ///  3) send the message to all connected clients (warning: possible race condition from accept thread)
        ///  4) remove any closed clients
        /// </summary>
        private async void SendMessageUILoop()
        {
            List<TcpClient> toRemove = new();
            List<TcpClient> toSendTo = new();

            while (true)
            {
                string message = ReadMessageFromConsole();

                //
                // Encode and Send the message
                //
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                //
                // Cannot have clients adding while we send messages, so make a copy of the
                // current list of clients.
                //
                lock ( clients )
                {
                    foreach ( var client in clients )
                    {
                        toSendTo.Add( client );
                    }
                }

                // Iterate over "saved" list of clients
                //
                // Question: Why can't we lock clients around this loop?
                //
                Console.WriteLine( $"  Sending a message of size ({message.Length}) to {toSendTo.Count} clients" );

                foreach ( TcpClient client in toSendTo )
                {
                    try
                    {
                        await client.GetStream().WriteAsync( messageBytes, 0, messageBytes.Length );
                        Console.WriteLine( $"    Message Sent from:   {client.Client.LocalEndPoint} to {client.Client.RemoteEndPoint}" );
                    }
                    catch ( Exception ex )
                    {
                        Console.WriteLine( $"    Client Disconnected: {client.Client.RemoteEndPoint} - {ex.Message}" );
                        toRemove.Add( client );
                    }
                }

                lock ( clients )
                {
                    // update list of "current" clients by removing closed clients
                    foreach ( TcpClient client in toRemove )
                    {
                        clients.Remove( client );
                    }
                }

                toSendTo.Clear();
                toRemove.Clear();
            }
        }

        /// <summary>
        ///   Read a message from the console.
        ///   The special message "largemessage" is used to create a long string.
        /// </summary>
        /// <returns>the message as a string</returns>
        private string ReadMessageFromConsole()
        {
            Console.WriteLine( "\nEnter a message to send to all clients: " );
            string? message = Console.ReadLine();

            if (message == null)
            {
                Console.WriteLine( "Exiting, but you will never hear this, as you are gone...." );
                Environment.Exit( 0 );
            }

            if ( message == "largemessage" )
            {
                message = GenerateLargeMessage();
            }

            return message;
        }

        /// <summary>
        /// Generate a big string of the letter a repeated...
        /// </summary>
        /// <returns></returns>
        private string GenerateLargeMessage()
        {
            StringBuilder retval = new StringBuilder();

            for ( int i = 0; i < message_length; i++ )
            {
                retval.Append( 'a' );
            }

            retval.Append( '.' );

            message_length += message_length; 

            return retval.ToString();
        }
    }
}
