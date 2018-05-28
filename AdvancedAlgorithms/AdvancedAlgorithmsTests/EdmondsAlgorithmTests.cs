using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;
using AdvancedAlgorithms;

namespace AdvancedAlgorithmsTests
{
    [TestClass]
    public class EdmondsAlgorithmTests
    {
        private static UndirectedGraph<int, Edge<int>> GetGraph1()
        {
            var g = new UndirectedGraph<int, Edge<int>>();
            g.AddVertexRange(new List<int> { 0, 1, 2, 3, 4, 5 });
            g.AddEdgeRange(new List<Edge<int>>
            {
                new Edge<int>(0,1),
                new Edge<int>(0,4),
                new Edge<int>(1,2),
                new Edge<int>(1,4),
                new Edge<int>(2,3),
                new Edge<int>(2,5),
                new Edge<int>(4,5),
            });
            return g;
        }

        private static UndirectedGraph<int, Edge<int>> GetGraph2()
        {
            var g = new UndirectedGraph<int, Edge<int>>();
            g.AddVertexRange(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            g.AddEdgeRange(new List<Edge<int>>
            {
                new Edge<int>(0,1),
                new Edge<int>(0,3),
                new Edge<int>(1,2),
                new Edge<int>(1,4),
                new Edge<int>(2,4),
                new Edge<int>(3,4),
                new Edge<int>(4,5),
                new Edge<int>(4,6),
                new Edge<int>(4,8),
                new Edge<int>(6,7),
                new Edge<int>(7,8),
            });
            return g;
        }

        [TestMethod]
        public void MaximumMatchingCountTest1()
        {
            var g = GetGraph1();
            int correctEdgesCount = 3;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            Assert.AreEqual(correctEdgesCount, maximumMatching.Count);

            var matchedVertices = GetMatchedVertices(maximumMatching);
            Assert.AreEqual(correctEdgesCount * 2, matchedVertices.Count);

            bool allEdgesInGraph = AreAllEdgesInGraph(g, maximumMatching);
            Assert.AreEqual(true, allEdgesInGraph);
        }

        [TestMethod]
        public void MaximumMatchingCountTest2()
        {
            var g = GetGraph2();
            int correctEdgesCount = 4;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            Assert.AreEqual(correctEdgesCount, maximumMatching.Count);

            var matchedVertices = GetMatchedVertices(maximumMatching);
            Assert.AreEqual(correctEdgesCount * 2, matchedVertices.Count);

            bool allEdgesInGraph = AreAllEdgesInGraph(g, maximumMatching);
            Assert.AreEqual(true, allEdgesInGraph);
        }

        private static bool AreAllEdgesInGraph(UndirectedGraph<int, Edge<int>> g, List<Edge<int>> edges)
        {
            bool allEdgesInGraph = true;

            foreach (var edge in edges)
            {
                bool edgeInGraph1 = g.TryGetEdge(edge.Source, edge.Target, out Edge<int> tempEdge1);
                bool edgeInGraph2 = g.TryGetEdge(edge.Target, edge.Source, out Edge<int> tempEdge2);
                if (!edgeInGraph1 || !edgeInGraph2)
                    return false;
            }

            return allEdgesInGraph;
        }

        [TestMethod]
        public void DFSSearchTest1()
        {
            var g = GetGraph2();

            Assert.AreEqual(true, CheckIfPathAlwaysExists(g));
        }

        [TestMethod]
        public void DFSSearchTest2()
        {
            var g = GetGraph1();

            Assert.AreEqual(true, CheckIfPathAlwaysExists(g));
        }

        [TestMethod]
        public void DFSSearchTest3()
        {
            var g = GetGraph2();
            g.AddVertex(15);

            Assert.AreEqual(false, CheckIfPathAlwaysExists(g));
        }

        [TestMethod]
        public void DFSSearchTest4()
        {
            var g = GetGraph1();
            g.AddVertex(15);

            Assert.AreEqual(false, CheckIfPathAlwaysExists(g));
        }

        private static bool CheckIfPathAlwaysExists(UndirectedGraph<int, Edge<int>> g)
        {
            bool pathAlwaysExists = true;
            var initialVisited = new Dictionary<int, bool>();
            foreach (var vertex in g.Vertices)
            {
                initialVisited.Add(vertex, false);
            }


            bool shouldContinue = true;
            foreach (var v1 in g.Vertices)
            {
                if (!shouldContinue)
                    break;
                foreach (var v2 in g.Vertices)
                {
                    var visited = new Dictionary<int, bool>(initialVisited);
                    bool pathFound = EdmondsAlgorithm.DFSSearch(v1, v2, visited, g, new Stack<Edge<int>>());
                    if (!pathFound)
                    {
                        pathAlwaysExists = pathFound;
                        shouldContinue = false;
                        break;
                    }
                }
            }

            return pathAlwaysExists;
        }

        private HashSet<int> GetMatchedVertices(List<Edge<int>> matching)
        {
            var matchedVertices = new HashSet<int>();
            foreach (var edge in matching)
            {
                matchedVertices.Add(edge.Source);
                matchedVertices.Add(edge.Target);
            }
            return matchedVertices;
        }

    }
}
