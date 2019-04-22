using System;

namespace Libs
{
    public class ExceptManager
    {
        public static void Write(Exception ex)
        {
            Console.WriteLine(String.Format("{0}:\n{1}", ex.GetType().ToString(), ex.Message));
            Console.ReadLine();
        }
    }
}
