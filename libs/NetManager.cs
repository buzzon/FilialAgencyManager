using Libs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class NetManager
    {
        public enum Commands
        {
            NULL,
            SubsidiaryAdd
        }

        public const char separator = '|';

        public static void Disconnect(TcpClient client)
        {
            if (client != null)
                try
                {
                    client.GetStream().Close();
                    client.Close();
                }
                catch
                {
                    client.Close();
                }
        }

        public static string GetClientIP(TcpClient client)
        {
            return ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        }

        public static string Receive(TcpClient client, NetworkStream stream)
        {
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(bytes, 0, client.ReceiveBufferSize);
            return Encoding.UTF8.GetString(bytes, 0, bytesRead);
        }

        public static string GetMessage(string receive)
        {
            return receive.Substring(GetCommand(receive).Length + 1);
        }

        public static string GetCommand(string receive)
        {
            return receive.Split(separator)[0];
        }

        public static void Send(NetworkStream stream, string message, Commands cmd = Commands.NULL)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", cmd.ToString(), separator, message));
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
