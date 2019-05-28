using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class NetManager
    {
        public static void Disconnect(TcpClient client)
        {
            if (client == null) return;
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

        public static string GetClientIp(TcpClient client)
        {
            return ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        }

        public static void Send(NetworkStream stream, byte[] bytes, CommandManager.Commands cmd = CommandManager.Commands.Null)
        {
            var bArray = AddByteToArray((byte)cmd, bytes);
            stream.Write(bArray, 0, bArray.Length);
        }

        public static byte[] Receive(TcpClient client, NetworkStream stream)
        {
            var bytes = new byte[client.ReceiveBufferSize];
            var bytesRead = stream.Read(bytes, 0, bytes.Length);

            var newArray = new byte[bytesRead];
            for (var i = 0; i < bytesRead; i++)
                newArray[i] = bytes[i];

            return newArray;
        }

        public static byte[] GetData(byte[] array)
        {
            var newArray = new byte[array.Length - 1];

            for (var i = 0; i < newArray.Length; i++)
                newArray[i] = array[i + 1];

            return newArray;
        }

        public static string ToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ToBytes(string messgae)
        {
            return Encoding.UTF8.GetBytes(messgae);
        }

        public static byte[] AddByteToArray(byte _byte, byte[] array)
        {
            var newArray = new byte[array.Length + 1];
            newArray[0] = _byte;

            for (var i = 0; i < array.Length; i++)
                newArray[i + 1] = array[i];

            return newArray;
        }
    }
}
