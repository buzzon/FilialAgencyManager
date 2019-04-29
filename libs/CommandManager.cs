using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class CommandManager
    {
        public enum Commands
        {
            NULL,
            SubsidiaryAdd,
            SubsidiaryLoad,
            QuaterDataSave
        }

        private const string Folder = "Subsidiary";


        public static void CommandHandler(NetworkStream stream, byte[] input, string clientIp)
        {
            string strInput = Encoding.UTF8.GetString(input);
            string message = NetManager.GetMessage(strInput);
            string command = NetManager.GetCommand(strInput);

            switch (command)
            {
                case nameof(Commands.SubsidiaryAdd):
                    SubsidiaryAdd(stream, message, clientIp);
                    break;
                case nameof(Commands.SubsidiaryLoad):
                    SubsidiaryLoad(stream);
                    break;
                case nameof(Commands.QuaterDataSave):
                    QuaterDataSave(stream, input, clientIp);
                    break;
                default:
                    Console.WriteLine("{0}: {1}({2});", clientIp, command, message);
                    break;
            }
        }

        private static void QuaterDataSave(NetworkStream stream, byte[] bytes , string clientIp)
        {
            string command = NetManager.GetCommand(Encoding.UTF8.GetString(bytes));
            int commandByteCount = Encoding.UTF8.GetByteCount(command) + 1 ;

            byte[] dataBytes = new byte[bytes.Length - commandByteCount];

            for (int i = 0; i < dataBytes.Length; i++)
                dataBytes[i] = bytes[i + commandByteCount];

            QuaterDataSerialize data = new QuaterDataSerialize();
            data = data.Deserialize(dataBytes);

            if (!Directory.Exists(Folder + "/" + data.subsidiary))
                Directory.CreateDirectory(Folder + "/" + data.subsidiary);

            try
            {
                File.WriteAllBytes(Folder + "/" + data.subsidiary + "/" + data.quater + ".dat", dataBytes);

                NetManager.Send(stream, String.Format("{0}: Получены данные \"{1}\" за {2} квартал.", clientIp, data.subsidiary, data.quater));
                Console.WriteLine("{0}: Получены данные \"{1}\" за {2} квартал.", clientIp, data.subsidiary, data.quater);
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
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

        private static void SubsidiaryAdd(NetworkStream stream, string message, string clientIp)
        {
            if (message != "")
            {
                SubsidiaryManager.Add(message);
                NetManager.Send(stream, String.Format("Добавлен новый филиал: {0}.", message));
                Console.WriteLine("{0}: Добавлен новый филиал: {1}.", clientIp, message);
            }
        }
    }
}
