using Libs;
using System;
using System.IO;
using System.Linq;

namespace Libs
{
    public class SubsidiaryManager
    {
        public const string FilePath = "Subsidiary.ini";

        public static void Add(string subsidiaryName)
        {
            try
            {
                File.AppendAllText(FilePath, subsidiaryName + "\n");
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
            }
        }

        public static void Remove(string subsidiaryName)
        {
            try
            {
                File.WriteAllLines(FilePath,
                    File.ReadLines(FilePath).Where(l => l != subsidiaryName).ToList());
            }
            catch (Exception ex)
            {
                ExceptManager.Write(ex);
            }
        }
    }
}
