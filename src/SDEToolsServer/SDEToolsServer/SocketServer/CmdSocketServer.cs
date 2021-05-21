using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    /// <summary>
    /// Runs a simple socket server
    /// accepts lines ended by CR+LF (Windows)
    /// accepts commands
    ///  - exit -> exit the connection
    /// Tested with telnet local connection
    /// </summary>
    public class CmdSocketServer
    {
        protected const byte CR = 0x0d;
        protected const byte LF = 0x0a;

        public Socket Server { get; protected set; }

        public int Port { get; protected set; }

        public int Backlog { get; protected set; }

        protected CmdSocketServer()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPort"> listening port</param>SimpleSocketServerCmd
        /// <param name="pBacklog">maximum number of connections</param>
        /// <returns></returns>
        public static CmdSocketServer GetSimpleSocketServer(int pPort, int pBacklog = 0)
        {
            return new CmdSocketServer() {Port = pPort, Backlog = pBacklog};
        }

        public void Listen()
        {
            try
            {
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Server.Bind(new IPEndPoint(IPAddress.Any, Port));
                Server.Listen(Backlog > 1 ? Backlog : 1);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ErrorCode + ": " + ex.Message);
                throw;
            }

            
            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine($"Waiting for a connection on port [{Port}]...");
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = Server.Accept();

                new Thread(() => ProcessRequests(handler)).Start();
            }
        }

        private void ProcessRequests(Socket handler)
        {
            handler.Send(Encoding.GetEncoding("iso-8859-1").GetBytes("Connected to the test server \r\n"));
            var data = new StringBuilder();
            byte[] bytes = new Byte[1024];
            bool quit = false;
            // An incoming connection needs to be processed.  
            while (!quit)
            {
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);

                    data.Append(Encoding.GetEncoding("iso-8859-1").GetString(bytes, 0, bytesRec));

                    if (bytesRec == 2 && bytes[0] == CR && bytes[1] == LF) break; // line received -> end of message
                }
                
                string command = data.ToString().Replace("\r\n", "");
                // Show the command on the console.  
                Console.WriteLine("Command received : {0}", data);
                
                string commandResult = "";
                if (command.Equals("quit"))
                {
                    quit = true;
                    commandResult = "End connection";
                }
                else
                {
                    commandResult = "Received command = " + command + "\r\n";
                }
                data.Clear();
                // Echo the data back to the client.  
                byte[] msg = Encoding.GetEncoding("iso-8859-1").GetBytes(commandResult);
                handler.Send(msg);
            }

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}
