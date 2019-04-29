using Libs;
using System;
using System.Net.Sockets;

namespace Server
{
    class СlientObject
    {
        private TcpClient client;
        private string clientIp;
        public СlientObject(TcpClient tcpClient)
        {
            client = tcpClient;
            clientIp = NetManager.GetClientIP(client);
        }

        public void Process()
        {
            try
            {
                Console.WriteLine("{0}: Подключился.", clientIp);
                while (true)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        byte[] input = NetManager.Receive(client, stream);
                        CommandManager.CommandHandler(stream, input, clientIp);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("{0}: Отключился.", clientIp);
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
