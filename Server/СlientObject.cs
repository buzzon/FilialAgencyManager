using Libs;
using System;
using System.Net.Sockets;

namespace Server
{
    internal class СlientObject
    {
        private readonly TcpClient _client;
        private readonly string _clientIp;
        public СlientObject(TcpClient tcpClient)
        {
            _client = tcpClient;
            _clientIp = NetManager.GetClientIp(_client);
        }

        public void Process()
        {
            try
            {
                Console.WriteLine("{0}: Подключился.", _clientIp);
                while (true)
                {
                    try
                    {
                        var stream = _client.GetStream();
                        var input = NetManager.Receive(_client, stream);
                        CommandManager.CommandHandler(stream, input, _clientIp);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("{0}: Отключился.", _clientIp);
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
                NetManager.Disconnect(_client);
            }
        }
    }
}
