using Libs;
using System;
using System.Net.Sockets;

namespace Server
{
    class ClientManager
    {
        public TcpClient client;
        public ClientManager(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {
            try
            {
                NetworkStream stream = client.GetStream();
                while (true)
                {
                    string message = NetManager.Receive(client, stream);
                    Console.WriteLine(message);
                    NetManager.Send(stream, "Сообщение получено.");
                }
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
            }
            finally
            {
                NetManager.Disconnect(client);
            }
        }
    }
}
