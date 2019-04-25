﻿using Libs;
using System;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class ClientProgram
    {
        const int port = 8888;
        const string address = "127.0.0.1";

        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(address), port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    string output = Console.ReadLine();

                    CommandManager.Commands command = CommandManager.Commands.NULL;
                    if (Enum.TryParse(NetManager.GetCommand(output), true, out command))
                    NetManager.Send(stream, NetManager.GetMessage(output), command);

                    string input = NetManager.Receive(client, stream);
                    Console.WriteLine("Сервер: {0}", NetManager.GetMessage(input));
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

        private static string GetUsername()
        {
            Console.Write("Введите свое имя:");
            return Console.ReadLine();
        }
    }
}
