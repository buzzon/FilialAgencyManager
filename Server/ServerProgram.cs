using Libs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class ServerProgram
    {
        private const int Port = 8888;

        private static TcpListener _listener;

        private static void Main()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, Port);
                _listener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    var client = _listener.AcceptTcpClient();
                    var clientObject = new СlientObject(client);

                    var clientThread = new Thread(clientObject.Process);
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
            }
            finally
            {
                _listener?.Stop();
            }
        }
    }
}
