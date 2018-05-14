using ASD.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms
{
    public static class FileParser
    {
        public static bool TryParseLine(string inputLine)//, out Graph g)
        {
            //g = new AdjacencyMatrixGraph(false, 100);

            int lineCounter = 0;
            int numberOfPeople = 0;
            int pairsNumber = 0;
            var lines = System.IO.File.ReadAllLines(inputLine);
            foreach(string line in lines)
            {
                if(lineCounter == 0)
                {
                    ParseSingleInteger(line, out numberOfPeople);
                    // create graph
                }
                else if(lineCounter == 1)
                {
                    ParseSingleInteger(line, out pairsNumber);
                }
                else
                {
                    if(lineCounter - 2 > pairsNumber)
                    {
                        Console.WriteLine("Reading lines done");
                        break;
                    }


                    var pair = ParsePairLine(line);
                    // add edge to graph
                    
                }
                lineCounter++;
            }


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
            return  new Tuple<int, int>(firstInt, secondInt);
        }
    }
}
