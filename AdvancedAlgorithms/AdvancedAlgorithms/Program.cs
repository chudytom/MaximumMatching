using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.Graphs;

namespace AdvancedAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            //Graph g = new AdjacencyMatrixGraph(false, 100);
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
