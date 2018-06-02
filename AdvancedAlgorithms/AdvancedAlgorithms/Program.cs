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
                if(FileParser.TryParseFile(consoleInput, out g, out pairs))
                {
                    // algorithm
                    var e1 = new List<Tuple<int, int>>();
                    var e2 = new List<Tuple<int, int>>();
                    int numberOfIterations = JTAlgorithm.Calculate(g, pairs, ref e1, ref e2);
                    Console.WriteLine("Policzono : " + numberOfIterations + " iteracji.");
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
