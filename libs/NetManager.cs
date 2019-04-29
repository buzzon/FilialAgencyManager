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

        public static string ToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ToBytes(string messgae)
        {
            return Encoding.UTF8.GetBytes(messgae);
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

        public static byte[] GetData(byte[] array)
        {
            byte[] newArray = new byte[array.Length - 1];

            for (int i = 0; i < newArray.Length; i++)
                newArray[i] = array[i + 1];

            return newArray;
        }


        public static void Send(NetworkStream stream, byte[] bytes, CommandManager.Commands cmd = CommandManager.Commands.NULL)
        {
            byte[] bArray = addByteToArray((byte)cmd, bytes);
            stream.Write(bArray, 0, bArray.Length);
        }

        public static byte[] addByteToArray(byte _byte, byte[] array)
        {
            byte[] newArray = new byte[array.Length + 1];
            newArray[0] = _byte;

            for (int i = 0; i < array.Length; i++)
                newArray[i + 1] = array[i];

            return newArray;
        }
    }
}
