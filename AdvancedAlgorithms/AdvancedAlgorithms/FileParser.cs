using QuickGraph;
using System;
using System.Collections.Generic;

namespace AdvancedAlgorithms
{
    public static class FileParser
    {
        public static bool TryParseFile(string inputLine, out UndirectedGraph<int, Edge<int>> graph, out List<Tuple<int, int>> edgesList)
        {
            UndirectedGraph<int, Edge<int>> g = new UndirectedGraph<int, Edge<int>>(false);
            int lineCounter = 0;
            int numberOfPeople = 0;
            int pairsNumber = 0;
            var lines = System.IO.File.ReadAllLines(inputLine);
            edgesList = new List<Tuple<int, int>>();
            foreach (string line in lines)
            {
                if (lineCounter == 0)
                {
                    ParseSingleInteger(line, out numberOfPeople);
                    for (int i = 0; i < numberOfPeople; i++)
                    {
                        g.AddVertex(i);
                    }
                }
                else if (lineCounter == 1)
                {
                    ParseSingleInteger(line, out pairsNumber);
                }
                else
                {
                    if (lineCounter - 2 > pairsNumber)
                    {
                        Console.WriteLine("Reading lines done");
                        break;
                    }

                    var pair = ParsePairLine(line);
                    edgesList.Add(pair);

                    // not needed (?)
                    g.AddEdge(new Edge<int>(pair.Item1, pair.Item2));
                }
                lineCounter++;
            }

            graph = CreateFinalGraph(numberOfPeople, edgesList);

            return true;
        }

        private static void ParseSingleInteger(string input, out int result)
        {
            if (!int.TryParse(input, out result))
            {
                throw new Exception("Problems with parsing single integer input line");
            }
        }

        private static Tuple<int, int> ParsePairLine(string input)
        {
            var pairLine = input.Split();
            int firstInt = int.Parse(pairLine[0]);
            int secondInt = int.Parse(pairLine[1]);
            return new Tuple<int, int>(firstInt, secondInt);
        }

        private static UndirectedGraph<int, Edge<int>> CreateFinalGraph(UndirectedGraph<int, Edge<int>> g)
        {
            var finalGraph = new UndirectedGraph<int, Edge<int>>();
            return finalGraph;
        }

        private static UndirectedGraph<int, Edge<int>> CreateFinalGraph(int numberOfPeople, List<Tuple<int, int>> pairs)
        {
            var finalGraph = new UndirectedGraph<int, Edge<int>>();

            for (int i = 0; i < pairs.Count; i++)
            {
                finalGraph.AddVertex(i);
            }

            for(int i = 0; i < pairs.Count; i++)
            {
                for(int j = 0; j < pairs.Count; j++)
                {
                    if(i != j && !HaveCommonVertex(pairs[i], pairs[j]))
                    {
                        finalGraph.AddEdge(new Edge<int>(i, j));
                    }
                }
            }

            return finalGraph;
        }

        private static bool HaveCommonVertex(Tuple<int, int> first, Tuple<int, int> second)
        {
            if (first.Item1 == second.Item1 ||
                first.Item1 == second.Item2 ||
                first.Item2 == second.Item1 ||
                first.Item2 == second.Item2)
            {
                return true;
            }
            return false;
        }
    }
}
