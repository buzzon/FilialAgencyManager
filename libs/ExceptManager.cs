using System;

namespace Libs
{
    public class ExceptManager
    {
        public static void Write(Exception ex)
        {
            Console.WriteLine($"[ERROR] : {ex.Message}");
            Console.ReadLine();
        }
    }
}
