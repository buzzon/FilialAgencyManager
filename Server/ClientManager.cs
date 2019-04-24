using Libs;
using System;
using System.Net;
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
                Console.WriteLine("{0} подключился.", NetManager.GetClientIP(client));

                while (true)
                {
                    try
                    {
                        string input = NetManager.Receive(client, stream);
                        string cmd = NetManager.GetCommand(input);
                        string message = NetManager.GetMessage(input);

                        Console.WriteLine("{0} {1}", cmd, message);
                        NetManager.Send(stream, "Сообщение получено.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("{0} отключился.", NetManager.GetClientIP(client));
                        break;
                    }

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
