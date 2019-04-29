using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class NetManager
    {
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
            int bytesRead = stream.Read(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes, 0, bytesRead);
        }

        public static byte[] ReceiveBytes(TcpClient client, NetworkStream stream)
        {
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);

            byte[] newArray = new byte[bytesRead];
            for (int i = 0; i < bytesRead; i++)
                newArray[i] = bytes[i];

            return newArray;
        }

        public static string GetMessage(string receive)
        {
            return receive.Substring(GetCommand(receive).Length + 1);
        }

        public static string GetCommand(string receive)
        {
            return receive.Split(separator)[0];
        }

        public static void Send(NetworkStream stream, string message, CommandManager.Commands cmd = CommandManager.Commands.NULL)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", cmd.ToString(), separator, message));
            stream.Write(bytes, 0, bytes.Length);
        }

        public static void Send(NetworkStream stream, byte[] bytes, CommandManager.Commands cmd = CommandManager.Commands.NULL)
        {
            byte[] bytesCommand = Encoding.UTF8.GetBytes(string.Format("{0}{1}", cmd.ToString(), separator));

            byte[] bArray = addByteToArray(bytesCommand, bytes);
            stream.Write(bArray, 0, bArray.Length);
        }

        public static byte[] addByteToArray(byte[] first, byte[] second)
        {
            byte[] newArray = new byte[first.Length + second.Length];

            for (int i = 0; i < first.Length; i++)
                newArray[i] = first[i];

            for (int i = 0; i < second.Length; i++)
                newArray[i + first.Length] = second[i];

            return newArray;
        }
    }
}
