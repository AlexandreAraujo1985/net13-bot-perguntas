using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Perguntas");

            string connectionString = (args.Length > 0) ? args[0] : "localhost";
        }
    }
}
