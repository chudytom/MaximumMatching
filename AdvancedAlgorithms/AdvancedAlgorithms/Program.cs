using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Podaj ścieżkę do pliku (albo napisz 'Zamknij' w celu zamknięcia programu)");
                var consoleInput = Console.ReadLine();
                if(FileParser.TryParseLine(consoleInput)) //, out g))
                {
                    // graph processing and creating output
                    //Console.WriteLine(g.VerticesCount.ToString());
                }
                else
                {
                    // check if program should end
                    if(consoleInput.Contains("Zamknij"))
                    {
                        Console.WriteLine("Zamykanie...");
                        break;
                    }
                }

            }
        }
    }
}
