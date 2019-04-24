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

        private void CommandHandler(string cmd)
        {
            switch (cmd)
            {
                case nameof(NetManager.Commands.ADD_Subsidiary):
                    Console.WriteLine("сработала команда {0} ", cmd);
                    break;
                default:
                    break;
            }
        }


        public void Process()
        {
            try
            {
                NetworkStream stream = client.GetStream();
                string clientIp = NetManager.GetClientIP(client);
                Console.WriteLine("{0}: подключился.", clientIp);

                while (true)
                {
                    try
                    {
                        string input = NetManager.Receive(client, stream);
                        string cmd = NetManager.GetCommand(input);
                        string message = NetManager.GetMessage(input);

                        Console.WriteLine("{0}: {1} {2}", clientIp, cmd, message);
                        CommandHandler(cmd);

                        NetManager.Send(stream, "Сообщение получено.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("{0}: отключился.", clientIp);
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
