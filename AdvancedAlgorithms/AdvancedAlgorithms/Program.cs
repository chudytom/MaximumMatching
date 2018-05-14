using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using QuickGraph.Algorithms;

namespace AdvancedAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            // example :D
            int nodes = 10;
            AdjacencyGraph<int, Edge<int>> graph = new AdjacencyGraph<int, Edge<int>>(true);
            for(int i = 0; i < nodes; i++)
            {
                graph.AddVertex(i);
            }

            for(int i = 0; i < nodes; i++)
            {
                for(int j = 0; j < nodes; j++)
                {
                    if(i!= j)
                    {
                        graph.AddEdge(new Edge<int>(i, j));
                        graph.AddEdge(new Edge<int>(j, i));
                    }
                }
            }


            UndirectedGraph<int, Edge<int>> g;
            List<Tuple<int, int>> pairs;
            while (true)
            {
                Console.WriteLine("Podaj ścieżkę do pliku (albo napisz 'Zamknij' w celu zamknięcia programu)");
                var consoleInput = Console.ReadLine();
                if(FileParser.TryParseLine(consoleInput, out g, out pairs))
                {
                    // algorithm
                    Console.WriteLine(g.EdgeCount);
                    JTAlgorithm.Calculate(g, pairs);
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
