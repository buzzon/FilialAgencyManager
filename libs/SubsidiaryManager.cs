using Libs;
using System;
using System.IO;

namespace Libs
{
    public class SubsidiaryManager
    {
        const string FilePath = "Subsidiary.ini";

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
        }
    }
}
