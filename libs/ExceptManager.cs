using System;

namespace Libs
{
    public class ExceptManager
    {
        public static void Write(Exception ex)
        {
            Console.WriteLine(String.Format("[ERROR] : {0}", ex.Message));
            Console.ReadLine();
        }
    }
}
