using Libs;
using System;
using System.IO;
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

        private void CommandHandler(NetworkStream stream, string input)
        {
            string message = NetManager.GetMessage(input);
            string command = NetManager.GetCommand(input);

            Console.WriteLine("{0}: {1}({2});", clientIp, command, message);

            switch (command)
            {
                case nameof(NetManager.Commands.SubsidiaryAdd):
                    SubsidiaryAdd(stream, message);
                    break;
                case nameof(NetManager.Commands.SubsidiaryLoad):
                    SubsidiaryLoad(stream);
                    break;
                default:
                    break;
            }
        }

        private static void SubsidiaryLoad(NetworkStream stream)
        {
            string Subsidiarys = "";
            if (File.Exists(SubsidiaryManager.FilePath))
                foreach (var item in File.ReadAllLines(SubsidiaryManager.FilePath))
                    Subsidiarys += item + NetManager.separator;

            NetManager.Send(stream, Subsidiarys);
        }

        private void SubsidiaryAdd(NetworkStream stream, string message)
        {
            SubsidiaryManager.Add(message);
            NetManager.Send(stream, String.Format("Добавлен новый филиал: {0}.", message));
            Console.WriteLine("{0}: Добавлен новый филиал: {1}.", clientIp, message);
        }

        public void Process()
        {
            try
            {
                Console.WriteLine("{0}: подключился.", clientIp);
                while (true)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        string input = NetManager.Receive(client, stream);
                        CommandHandler(stream, input);
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
