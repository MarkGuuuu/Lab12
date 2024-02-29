namespace CS3500
{
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// <para>
    ///   Author: H. James de St. Germain
    /// </para>
    /// <para>
    ///   Date:   Spring 2022
    /// </para>
    /// <para>
    ///   This code connects to a client and then Listens.
    /// </para>
    /// <para>
    ///    Listens = continuously wait for data. If the data contains a period
    ///              treat it as a sentence.
    /// </para>
    /// <para>
    ///   Question: Why are there no locks in this code?
    /// </para>
    /// </summary>
    public class ChatClient
    {
        /// <summary>
        /// Connection object to remote server.
        /// </summary>
        private readonly TcpClient tcpClient;

        /// <summary>
        ///   <para>
        ///     Constructor initiates and connects to remote server.
        /// 
        ///     Warning: does not gracefully report failure to connect.
        ///   </para>
        /// </summary>
        /// <param name="hostname">e.g., localhost or abc.cs.utah.edu</param>
        /// <param name="port">e.g., 11000</param>
        ///
        public ChatClient( string hostname, int port )
        {
            this.tcpClient = new TcpClient( hostname, port );

            if ( this.tcpClient.Connected )
            {
                Console.WriteLine( $"Connected to {this.tcpClient.Client.RemoteEndPoint}" );
                Console.WriteLine( $"Awaiting Data..." );
            }
            else
            {
                Console.WriteLine( $"Not Connected. Terminating Program." );
                Environment.Exit( 0 );
            }
        }

        /// <summary>
        ///   Continuously listen for network communications.
        ///
        ///   Save all incoming data and then check for "official" messages as
        ///   determined by "special" token (i.e., the period).
        /// </summary>
        public async void Listen()
        {
            try
            {
                StringBuilder dataBacklog = new StringBuilder();
                byte[] buffer = new byte[4096];
                NetworkStream stream = tcpClient.GetStream();

                if ( stream == null )
                {
                    return;
                }

                while ( true )
                {
                    int total = await stream.ReadAsync(buffer);

                    if (total == 0)
                    {
                        // the connection quit unexpectedly
                        throw new Exception( "End of Stream Reached. Connection must be closed." );
                    }

                    string current_data = Encoding.UTF8.GetString(buffer, 0, total);

                    dataBacklog.Append( current_data );

                    Console.WriteLine( $"  Received {total} new bytes for a total of {dataBacklog.Length}." );

                    this.CheckForMessage( dataBacklog );
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"oops{ex}" );
            }
        }

        /// <summary>
        ///   Given a string (actually a string builder object)
        ///   check to see if it contains one or more messages as defined by
        ///   our protocol (the period '.').
        /// </summary>
        /// <param name="data"> all characters encountered so far</param>
        private void CheckForMessage( StringBuilder data )
        {
            string  allData = data.ToString();
            int     terminator_position = allData.IndexOf( "." );
            bool    foundOneMessage = false;

            while ( terminator_position >= 0 )
            {
                foundOneMessage = true;

                string message = allData.Substring(0, terminator_position+1);
                data.Remove( 0, terminator_position + 1 );
                Console.WriteLine( $"  Message found:\n" +
                    $"  ---------------------------------------------------------------------------------\n" +
                    $"  {message}\n" );

                allData = data.ToString();
                terminator_position = allData.IndexOf( "." );
            }

            if ( !foundOneMessage )
            {
                Console.WriteLine( $"  Message NOT found" );
            }
            else
            {
                Console.WriteLine(
                    $"  --------------------------------------------------------------------------------\n" +
                    $"  After Message: {data.Length} bytes unprocessed.\n" +
                    $"  --------------------------------------------------------------------------------\n" );
            }
        }
    }
}
