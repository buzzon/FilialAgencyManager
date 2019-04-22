using Libs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class ServerProgram
    {
        const int port = 8888;
        const string address = "127.0.0.1";

        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(address), port);
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientManager clientObject = new ClientManager(client);

                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
