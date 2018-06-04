using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedAlgorithms
{
    public static class JTAlgorithm
    {
        public static int Calculate(UndirectedGraph<int, Edge<int>> g, List<Tuple<int, int>> pairs, ref List<Tuple<int, int>> e1, ref List<Tuple<int, int>> e2)
        {
            int numberOfIterations = 0;
            e1 = new List<Tuple<int, int>>();
            e2 = new List<Tuple<int, int>>();
            var edgesList = EdmondsAlgorithm.CalculateMaximumMatching(g);
            while(edgesList.Count > 0)
            {
                Console.WriteLine("Iteration: " + numberOfIterations);
                var edge = edgesList.First();               
                e1.Add(pairs[edge.Source]);
                e2.Add(pairs[edge.Target]);


                Console.WriteLine("Pairs: " + "I: " + pairs[edge.Source] + " II: " + pairs[edge.Target]);

                // we will remove vertices if they were not removed before
                // we will not remove edges (we dont care about them, we have edgesList)
                // remove edge (from edgeList, from graph + its vertices from graph)
                edgesList.Remove(edge);
                //g.RemoveEdge(edge); -> we dont need it
                if(g.Vertices.Contains(edge.Source))
                {
                    g.RemoveVertex(edge.Source);
                }
                if(g.Vertices.Contains(edge.Target))
                {
                    g.RemoveVertex(edge.Target);
                }             
                numberOfIterations++;
            }

            while(g.VertexCount > 0)
            {
                Console.WriteLine("Iteration: " + numberOfIterations);
                e1.Add(pairs[g.Vertices.First()]);
                Console.WriteLine("Pairs: " + "I: " + pairs[g.Vertices.First()] + " II: :(");
                g.RemoveVertex(g.Vertices.First());
                numberOfIterations++;
            }
            return numberOfIterations;
        }
    }
}
