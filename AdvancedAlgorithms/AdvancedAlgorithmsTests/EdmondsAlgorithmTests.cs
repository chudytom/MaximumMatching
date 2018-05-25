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
        private static UndirectedGraph<int, Edge<int>> GetGraphFromPapers()
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

        [TestMethod]
        public void MaximumMatchingCountTest()
        {
            var testGraphFromPapers = GetGraphFromPapers();
            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(testGraphFromPapers);
            int correctEdgesCount = 3;
            var matchedVertices = GetMatchedVertices(maximumMatching);

            Assert.AreEqual(maximumMatching.Count, correctEdgesCount);

            Assert.AreEqual(matchedVertices.Count, correctEdgesCount * 2);
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
