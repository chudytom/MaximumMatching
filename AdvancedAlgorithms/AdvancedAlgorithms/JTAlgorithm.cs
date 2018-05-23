using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms
{
    public static class JTAlgorithm
    {
        public static int Calculate(UndirectedGraph<int, Edge<int>> g, List<Tuple<int, int>> pairs)
        {
            int numberOfIterations = 0;
            List<Tuple<int, int>> e1 = new List<Tuple<int, int>>();
            List<Tuple<int, int>> e2 = new List<Tuple<int, int>>();
            var edgesList = EdmondsAlgorithm.CalculateMaximumMatching(g);
            while(edgesList.Count > 0)
            {
                Console.WriteLine("Iteration: " + numberOfIterations);
                var edge = edgesList.First();
                e1.Add(pairs[edge.Source]);
                e2.Add(pairs[edge.Target]);


                Console.WriteLine("Pairs: " + "I: " + edge.Source.ToString() + " II: " + edge.Target.ToString());


                // remove edges and vertices
                g.RemoveEdge(edge);
                g.RemoveVertex(edge.Source);
                g.RemoveVertex(edge.Target);
                numberOfIterations++;
            }

            while(g.VertexCount > 0)
            {
                e1.Add(pairs[g.Vertices.First()]);
                Console.WriteLine("Pairs: " + "I: " + pairs[g.Vertices.First()] + " II: :(");
                g.RemoveVertex(g.Vertices.First());
                numberOfIterations++;
            }
            return numberOfIterations;
        }
    }
}
