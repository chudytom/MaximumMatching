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
        public static void Calculate(UndirectedGraph<int, Edge<int>> g, List<Tuple<int, int>> pairs)
        {
            List<Tuple<int, int>> e1 = new List<Tuple<int, int>>();
            List<Tuple<int, int>> e2 = new List<Tuple<int, int>>();
            var edgesList = EdmondsAlgorithm.CalculateMaximumMatching(g);
            while(edgesList.Count > 0)
            {
                var edge = edgesList.First();
                e1.Add(pairs[edge.Source]);
                e2.Add(pairs[edge.Target]);

                // remove edges and vertices

            }


        }
    }
}
