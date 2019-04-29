using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class CommandManager
    {
        public enum Commands : byte
        {
            NULL,
            SubsidiaryAdd,
            SubsidiaryLoad,
            QuaterDataSave,
            AnnualReport,
        }

        private const string Folder = "Филиалы";


        public static void CommandHandler(NetworkStream stream, byte[] input, string clientIp)
        {
            byte[] inputData = NetManager.GetData(input);

            switch ((Commands)input[0])
            {
                case Commands.SubsidiaryAdd:
                    SubsidiaryAdd(stream, inputData, clientIp);
                    break;
                case Commands.SubsidiaryLoad:
                    SubsidiaryLoad(stream, clientIp);
                    break;
                case Commands.QuaterDataSave:
                    QuaterDataSave(stream, inputData, clientIp);
                    break;
                case Commands.AnnualReport:
                    AnnualReport(stream, inputData, clientIp);
                    break;
                default:
                    Console.WriteLine("{0}: {1}({2});", clientIp, ((Commands)input[0]).ToString(), inputData);
                    break;
            }
        }

        private static void AnnualReport(NetworkStream stream, byte[] inputData, string clientIp)
        {
            string subsidiary = NetManager.ToString(inputData);

            if (!Directory.Exists(Folder + "/" + subsidiary))
            {
                string message = String.Format("Данных для \"{0}\" не обнаружено.", subsidiary);
                NetManager.Send(stream, NetManager.ToBytes(message));
                Console.WriteLine("{0}: {1}", clientIp, message);
            }
            else
            {
                string quaters = string.Empty;

                foreach (var item in new DirectoryInfo(Folder + "/" + subsidiary).GetFiles())
                    quaters += " " + Path.GetFileNameWithoutExtension(item.Name);
                
                string message = String.Format("Отчет построен по данным за{0}.", quaters);
                NetManager.Send(stream, NetManager.ToBytes(message));
                Console.WriteLine("{0}: {1}", clientIp, message);
            }
        }

        private static void QuaterDataSave(NetworkStream stream, byte[] inputData , string clientIp)
        {
            try
            {
                QuaterDataSerialize quaterData = new QuaterDataSerialize();
                quaterData = quaterData.Deserialize(inputData);

                if (!Directory.Exists(Folder + "/" + quaterData.subsidiary))
                    Directory.CreateDirectory(Folder + "/" + quaterData.subsidiary);

                File.WriteAllBytes(Folder + "/" + quaterData.subsidiary + "/" + quaterData.quater + ".dat", inputData);
                string message = String.Format("Получены данные \"{0}\" за {1} квартал.", quaterData.subsidiary, quaterData.quater);
                NetManager.Send(stream, NetManager.ToBytes(message));
                Console.WriteLine("{0}: {1}", clientIp, message);
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
            }
        }

        private static void SubsidiaryLoad(NetworkStream stream, string clientIp)
        {
            string Subsidiarys = String.Empty;
            if (File.Exists(SubsidiaryManager.FilePath))
                foreach (var item in File.ReadAllLines(SubsidiaryManager.FilePath))
                    Subsidiarys += item + "\n";

            NetManager.Send(stream, NetManager.ToBytes(Subsidiarys));
            Console.WriteLine("{0}: Загружен список филиалов.", clientIp);
        }

        private static void SubsidiaryAdd(NetworkStream stream, byte[] inputData, string clientIp)
        {
            string subsidiary = Encoding.UTF8.GetString(inputData);

            if (subsidiary != String.Empty)
            {
                SubsidiaryManager.Add(subsidiary);
                string message = String.Format("Добавлен новый филиал: {0}.", subsidiary);
                NetManager.Send(stream, NetManager.ToBytes(message));
                Console.WriteLine("{0}: {1}.", clientIp, message);
            }
        }
    }
}
