using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace AdvancedAlgorithms
{
    public class GraphGenerator
    {
        public UndirectedGraph<int, Edge<int>> GetFullGraph(int verticesCount)
        {
            var edgeSet = new HashSet<Edge<int>>(new EdgeComparer());
            if (verticesCount < 1)
                throw new ArgumentException("There must be at lest 1 vertex in a graph");
            var g = new UndirectedGraph<int, Edge<int>>();
            g.AddVertexRange(Enumerable.Range(0, verticesCount));

            foreach (var sourceVertex in g.Vertices)
            {
                foreach (var targetVertex in g.Vertices)
                {
                    if (sourceVertex == targetVertex)
                        continue;
                    edgeSet.Add(new Edge<int>(sourceVertex, targetVertex));
                }
            }
            g.AddEdgeRange(edgeSet);

            return g;
        }

        public UndirectedGraph<int, Edge<int>> GetCycleGraph(int verticesCount)
        {
            var g = new UndirectedGraph<int, Edge<int>>();
            g.AddVertexRange(Enumerable.Range(0, verticesCount));
            for (int i = 0; i < verticesCount - 1; i++)
            {
                g.AddEdge(new Edge<int>(i, i + 1));
            }
            g.AddEdge(new Edge<int>(0, verticesCount - 1));
            return g;   
        }
    }
}
