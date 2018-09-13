using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Perguntas");

            string connectionString = (args.Length > 0) ? args[0] : "localhost";
            var botPerguntas = new BotPergunta(connectionString);

            for(int i=1; ;i++)
            {
                Console.Write($"P{i}: ");
                string texto = Console.ReadLine();

                if (texto == null || texto == "")
                    break;

                botPerguntas.Perguntar(i, texto);

                var respostas = botPerguntas.EsperarRespostasAsync(i).Result;

                if(respostas.Length == 0)
                {
                    Console.WriteLine("Nenhuma resposta");
                }
                else
                {
                    foreach(var r in respostas)
                    {
                        Console.WriteLine($"  - [{r.Autor}] disse: {r.Texto}");
                    }
                }

                Console.WriteLine("----------------------------");
            }
        }
    }
}
