using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class NetManager
    {
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

        public static string Receive(TcpClient client, NetworkStream stream)
        {
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(bytes, 0, client.ReceiveBufferSize);
            return Encoding.UTF8.GetString(bytes, 0, bytesRead);
        }

        public static void Send(NetworkStream stream, string message)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(sendBytes, 0, sendBytes.Length);
        }
    }
}
