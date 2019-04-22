using Libs;
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
            string userName = GetUsername();
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(address), port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    string message = GetMessage(userName);
                    NetManager.Send(stream, message);

                    string request = NetManager.Receive(client, stream);
                    Console.WriteLine("Сервер: {0}", request);
                }
            }
            catch (SocketException ex)
            {
                ExceptManager.Write(ex);
            }
            catch (InvalidOperationException ex)
            {
                ExceptManager.Write(ex);
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

        private static string GetMessage(string userName)
        {
            Console.Write(userName + ": ");
            return String.Format("{0}: {1}", userName, Console.ReadLine()); ;
        }

        private static string GetUsername()
        {
            Console.Write("Введите свое имя:");
            return Console.ReadLine();
        }
    }
}
