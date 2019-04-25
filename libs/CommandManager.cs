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


        public static void CommandHandler(NetworkStream stream, string input, string clientIp)
        {
            string message = NetManager.GetMessage(input);
            string command = NetManager.GetCommand(input);

            Console.WriteLine("{0}: {1}({2});", clientIp, command, message);

            switch (command)
            {
                case nameof(Commands.SubsidiaryAdd):
                    SubsidiaryAdd(stream, message, clientIp);
                    break;
                case nameof(Commands.SubsidiaryLoad):
                    SubsidiaryLoad(stream);
                    break;
                case nameof(Commands.QuaterDataSave):
                    QuaterDataSave(stream, message, clientIp);
                    break;
                default:
                    break;
            }
        }

        private static void QuaterDataSave(NetworkStream stream, string message, string clientIp)
        {
            string subsidiary = message.Split(NetManager.separator)[0];
            string quarter = message.Split(NetManager.separator)[1];
            byte[] quaterData = Encoding.UTF8.GetBytes(message.Split(NetManager.separator)[2]);
            MemoryStream memoryStream = new MemoryStream(quaterData);
            QuaterDataSerialize quaterDataSerialize = new QuaterDataSerialize(null, null);
            quaterDataSerialize.Deserialize(memoryStream);

            NetManager.Send(stream, String.Format("{0}: Получены данные {1} за {2} квартал.", clientIp, subsidiary, quarter));
            Console.WriteLine("{0}: Получены данные {1} за {2} квартал.", clientIp, subsidiary, quarter);
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
