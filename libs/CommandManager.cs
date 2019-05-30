using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Libs
{
    public class CommandManager
    {
        public enum Commands : byte
        {
            Null,
            SubsidiaryAdd,
            SubsidiaryLoad,
            QuaterDataSave,
            AnnualReport,
        }

        private const string Folder = "Филиалы";


        public static void CommandHandler(NetworkStream stream, byte[] input, string clientIp)
        {
            var inputData = NetManager.GetData(input);

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
                case Commands.Null:
                    break;
                default:
                    Console.WriteLine("{0}: {1}({2});", clientIp, ((Commands)input[0]).ToString(), inputData);
                    break;
            }
        }

        private static void AnnualReport(NetworkStream stream, byte[] inputData, string clientIp)
        {
            var subsidiary = NetManager.ToString(inputData);
            var annualReport = new QuaterDataSerialize();

            if (!Directory.Exists(Folder + "/" + subsidiary))
            {
                var message = $"Данных для \"{subsidiary}\" не обнаружено.";
                NetManager.Send(stream, NetManager.ToBytes(message));
                Console.WriteLine("{0}: {1}", clientIp, message);
            }
            else
            {
                var quaters = string.Empty;

                foreach (var item in new DirectoryInfo(Folder + "/" + subsidiary).GetFiles())
                {
                    quaters += ", " + Path.GetFileNameWithoutExtension(item.Name);

                    var quater = new QuaterDataSerialize();
                    var vs = File.ReadAllBytes(Folder + "/" + subsidiary + "/" + item.Name);

                    annualReport.AddData(quater.Deserialize(vs));
                }

                quaters = quaters.Substring(1, quaters.Length - 1);

                var message = $"Отчет построен по данным за{quaters}.";
                NetManager.Send(stream, NetManager.ToBytes(message));
                Console.WriteLine("{0}: {1}", clientIp, message);
            }

            NetManager.Send(stream, annualReport.Serialize());
        }

        private static void QuaterDataSave(NetworkStream stream, byte[] inputData , string clientIp)
        {
            try
            {
                QuaterDataSerialize quaterData = new QuaterDataSerialize();
                quaterData = quaterData.Deserialize(inputData);

                if (!Directory.Exists(Folder + "/" + quaterData.Subsidiary))
                    Directory.CreateDirectory(Folder + "/" + quaterData.Subsidiary);

                File.WriteAllBytes(Folder + "/" + quaterData.Subsidiary + "/" + quaterData.Quater + ".dat", inputData);
                var message = $"Получены данные \"{quaterData.Subsidiary}\" за {quaterData.Quater} квартал.";
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
            var subsidiarys = string.Empty;
            if (File.Exists(SubsidiaryManager.FilePath))
                subsidiarys = File.ReadAllLines(SubsidiaryManager.FilePath).Aggregate(subsidiarys, 
                    (current, item) => current + (item + "\n"));

            NetManager.Send(stream, NetManager.ToBytes(subsidiarys));
            Console.WriteLine("{0}: Загружен список филиалов.", clientIp);
        }

        private static void SubsidiaryAdd(NetworkStream stream, byte[] inputData, string clientIp)
        {
            var subsidiary = Encoding.UTF8.GetString(inputData);
            if (subsidiary == string.Empty) return;

            SubsidiaryManager.Add(subsidiary);
            var message = $"Добавлен новый филиал: {subsidiary}.";
            NetManager.Send(stream, NetManager.ToBytes(message));
            Console.WriteLine("{0}: {1}.", clientIp, message);
        }
    }
}
