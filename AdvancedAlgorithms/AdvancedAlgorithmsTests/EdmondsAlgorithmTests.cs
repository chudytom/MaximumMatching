using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;
using AdvancedAlgorithms;
using System.Linq;

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
        public void MaximumMatchingCount_ShouldReturn_3()
        {
            var g = GetGraph1();
            int correctEdgesCount = 3;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_ShouldReturn_4()
        {
            var g = GetGraph2();
            int correctEdgesCount = 4;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_5Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 5;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 2;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_6Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 6;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 3;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_7Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 7;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 3;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_8Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 8;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 4;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }


        [TestMethod]
        public void MaximumMatchingCount_FullGraph_10Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 10;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 5;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_15Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 15;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 7;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_20Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 20;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 10;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_FullGraph_50Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 50;
            var g = generator.GetFullGraph(verticesCount);
            int correctEdgesCount = 25;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_CycleGraph_2Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 2;
            var g = generator.GetCycleGraph(verticesCount);
            int correctEdgesCount = 1;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_CycleGraph_3Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 3;
            var g = generator.GetCycleGraph(verticesCount);
            int correctEdgesCount = 1;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_CycleGraph_49Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 49;
            var g = generator.GetCycleGraph(verticesCount);
            int correctEdgesCount = 24;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        [TestMethod]
        public void MaximumMatchingCount_CycleGraph_100Vertices()
        {
            var generator = new GraphGenerator();
            int verticesCount = 100;
            var g = generator.GetCycleGraph(verticesCount);
            int correctEdgesCount = 50;

            var maximumMatching = EdmondsAlgorithm.CalculateMaximumMatching(g);
            var testResult = VerifyMatching(maximumMatching, g, correctEdgesCount);

            Assert.IsTrue(testResult.IsCorrect, testResult.ErrorMessage);
        }

        private AdvancedAlgorithms.TestResult VerifyMatching(List<Edge<int>> matching, UndirectedGraph<int, Edge<int>> g, int correctEdgesCount)
        {
            if (correctEdgesCount != matching.Count)
                return new AdvancedAlgorithms.TestResult(false, $"Incorrect matching edges count. " +
                    $"Expected <{correctEdgesCount}>. Acutal <{matching.Count}>");
            var matchedVertices = EdmondsAlgorithm.GetAllVerticesForPath(matching);
            if (correctEdgesCount * 2 != matchedVertices.Count)
                return new AdvancedAlgorithms.TestResult(false, $"Some vertices in more than one edge." +
                    $" Expected <{correctEdgesCount * 2}>. Acutal <{matchedVertices.Count}>");
            bool allEdgesInGraph = AreAllEdgesInGraph(g, matching);
            if (!allEdgesInGraph)
                return new AdvancedAlgorithms.TestResult(false, "Not all edges are a part of the original graph");
            else
                return new AdvancedAlgorithms.TestResult(true, "");
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

            Assert.IsTrue(CheckIfPathAlwaysExists(g));
        }

        [TestMethod]
        public void DFSSearchTest2()
        {
            var g = GetGraph1();

            Assert.IsTrue(CheckIfPathAlwaysExists(g));
        }

        [TestMethod]
        public void DFSSearchTest3()
        {
            var g = GetGraph2();
            g.AddVertex(15);

            Assert.IsFalse(CheckIfPathAlwaysExists(g));
        }

        [TestMethod]
        public void DFSSearchTest4()
        {
            var g = GetGraph1();
            g.AddVertex(15);

            Assert.IsFalse(CheckIfPathAlwaysExists(g));
        }

        private static bool CheckIfPathAlwaysExists(UndirectedGraph<int, Edge<int>> g)
        {
            bool pathAlwaysExists = true;

            bool shouldContinue = true;
            foreach (var v1 in g.Vertices)
            {
                if (!shouldContinue)
                    break;
                foreach (var v2 in g.Vertices)
                {
                    bool pathFound = EdmondsAlgorithm.DFSSearch(v1, v2, g, out List<Edge<int>> path);
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


        [TestMethod]
        public void GetSymmetricalDifference_ShouldReturn_Count_1()
        {
            var list1 = new List<Edge<int>>
            {
                new Edge<int>(0, 1),
                new Edge<int>(1, 2),

            };
            var list2 = new List<Edge<int>>()
            {
                new Edge<int>(1,0),
            };

            var difference = EdmondsAlgorithm.GetSymmetricalDifference(list1, list2);

            Assert.AreEqual(1, difference.Count);
            Assert.IsTrue(IsDifferenceCorrect(difference, list1, list2));
        }

        [TestMethod]
        public void GetSymmetricalDifference_ShouldReturn_Count_2()
        {
            var list1 = new List<Edge<int>>
            {
                new Edge<int>(2, 3),
                new Edge<int>(0, 1),
                new Edge<int>(4, 5),
            };
            var list2 = new List<Edge<int>>()
            {
                new Edge<int>(2, 3),
                new Edge<int>(0, 1),
                new Edge<int>(4, 5),
                new Edge<int>(1, 2),
                new Edge<int>(2, 5),
            };

            var difference = EdmondsAlgorithm.GetSymmetricalDifference(list1, list2);

            Assert.AreEqual(2, difference.Count);

            Assert.IsTrue(IsDifferenceCorrect(difference, list1, list2));
        }

        [TestMethod]
        public void GetSymmetricalDifference_ShouldReturn_Count_0()
        {
            var list1 = new List<Edge<int>>
            {
                new Edge<int>(2, 3),
                new Edge<int>(0, 1),
                new Edge<int>(4, 5),
                new Edge<int>(1, 2),
                new Edge<int>(2, 5),
                new Edge<int>(7, 8),
                new Edge<int>(2, 5),
                new Edge<int>(3, 88),
                new Edge<int>(8, 5),
            };
            var list2 = new List<Edge<int>>()
            {
                new Edge<int>(2, 3),
                new Edge<int>(0, 1),
                new Edge<int>(4, 5),
                new Edge<int>(1, 2),
                new Edge<int>(2, 5),
                new Edge<int>(7, 8),
                new Edge<int>(2, 5),
                new Edge<int>(3, 88),
                new Edge<int>(8, 5),
            };

            var difference = EdmondsAlgorithm.GetSymmetricalDifference(list1, list2);

            Assert.AreEqual(0, difference.Count);

            Assert.IsTrue(IsDifferenceCorrect(difference, list1, list2));
        }

        private bool IsDifferenceCorrect(List<Edge<int>> difference, List<Edge<int>> list1, List<Edge<int>> list2)
        {
            var set1 = new HashSet<Edge<int>>(list1, new EdgeComparer());
            var set2 = new HashSet<Edge<int>>(list2, new EdgeComparer());
            foreach (var edge in difference)
            {
                bool inSet1 = set1.Contains(edge);
                bool inSet2 = set2.Contains(edge);
                if ((inSet1 && inSet2) || (!inSet1 && !inSet2))
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void ContractMatching_Count_ShouldReturn_2()
        {
            var matching = new List<Edge<int>>()
            {
                new Edge<int>(1,2),
                new Edge<int>(3,4),
                new Edge<int>(5,6),
                new Edge<int>(8,7),
            };
            var blossom = new List<Edge<int>>()
            {
                new Edge<int>(4,5),
                new Edge<int>(5,6),
                new Edge<int>(6,7),
                new Edge<int>(8,7),
                new Edge<int>(8,4),
            };

            var newMatching = EdmondsAlgorithm.ContractMatching(matching, blossom);
            int expectedMatchingCount = matching.Count - blossom.Count / 2;

            Assert.AreEqual(expectedMatchingCount, newMatching.Count);
        }

        [TestMethod]
        public void ContractMatching_Count_ReverseOrder_ShouldReturn_2()
        {
            var matching = new List<Edge<int>>()
            {
                new Edge<int>(1,2),
                new Edge<int>(3,4),
                new Edge<int>(5,6),
                new Edge<int>(8,7),
            };
            var blossom = new List<Edge<int>>()
            {
                new Edge<int>(8,4),
                new Edge<int>(7,6),
                new Edge<int>(4,5),
                new Edge<int>(6,5),
                new Edge<int>(8,7),
            };

            var newMatching = EdmondsAlgorithm.ContractMatching(matching, blossom);
            int expectedMatchingCount = matching.Count - blossom.Count / 2;

            Assert.AreEqual(expectedMatchingCount, newMatching.Count);
        }

        [TestMethod]
        public void GetTargetVertex_ShouldReturn_1()
        {
            Assert.AreEqual(1, EdmondsAlgorithm.GetTargetVertex(new Edge<int>(0, 1), 0));
            Assert.AreEqual(1, EdmondsAlgorithm.GetTargetVertex(new Edge<int>(1, 0), 0));

        }

        [TestMethod]
        public void GetTargetVertex_ShouldReturn_0()
        {
            Assert.AreEqual(0, EdmondsAlgorithm.GetTargetVertex(new Edge<int>(0, 1), 1));
            Assert.AreEqual(0, EdmondsAlgorithm.GetTargetVertex(new Edge<int>(1, 0), 1));
        }

        [TestMethod]
        public void TryGetRootForVertex_2_ShouldFind_2()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 2;
            int expectedRoot = 2;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_0_ShouldFind_2()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 0;
            int expectedRoot = 2;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_11_ShouldFind_11()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 11;
            int expectedRoot = 11;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_3_ShouldFind_2()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 3;
            int expectedRoot = 2;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_7_ShouldFind_2()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 7;
            int expectedRoot = 2;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_10_ShouldFind_2()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 10;
            int expectedRoot = 2;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_18_ShouldFind_12()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 18;
            int expectedRoot = 12;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_15_ShouldFind_12()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 15;
            int expectedRoot = 12;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_14_ShouldFind_12()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 15;
            int expectedRoot = 12;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);

            Assert.IsTrue(IsPathCorrect(pathToRoot, forest, sourceVertex, expectedRoot));
        }

        [TestMethod]
        public void TryGetRootForVertex_13_ShouldFind_12()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = 13;
            int expectedRoot = 12;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsTrue(rootFound);
            Assert.AreEqual(expectedRoot, foundRootVertex);
        }

        [TestMethod]
        public void TryGetRootForVertex_minus1_ShouldNotFindPath()
        {
            var forest = GetSampleForest(out int[] levels);
            int sourceVertex = -1;

            bool rootFound = EdmondsAlgorithm.TryGetRootForVertex(forest, sourceVertex, levels, out int foundRootVertex, out List<Edge<int>> pathToRoot);

            Assert.IsFalse(rootFound);
        }

        [TestMethod]
        public void IsPathCorrect_Throws_ArgumentException()
        {
            var g = GetSampleForest(out int[] levels);
            var path = new List<Edge<int>>
            {
                new Edge<int>(0, 0)
            };
            Assert.ThrowsException<ArgumentException>(() => IsPathCorrect(path, g, 1, 2));
        }

        private UndirectedGraph<int, Edge<int>> GetSampleForest(out int[] levels)
        {
            var forest = new UndirectedGraph<int, Edge<int>>();
            int vertexCount = 19;
            levels = new int[vertexCount];
            forest.AddVertexRange(Enumerable.Range(0, vertexCount));

            levels[2] = levels[11] = levels[12] = 0;
            levels[1] = levels[3] = levels[13] = 1;
            levels[0] = levels[4] = levels[14] = 2;
            levels[5] = levels[7] = levels[9] = levels[15] = levels[17] = 3;
            levels[6] = levels[8] = levels[10] = levels[16] = levels[18] = 4;

            forest.AddEdgeRange(new List<Edge<int>>
            {
                new Edge<int>(0,1),
                new Edge<int>(2,1),
                new Edge<int>(2,3),
                new Edge<int>(4,3),
                new Edge<int>(4,5),
                new Edge<int>(4,7),
                new Edge<int>(4,9),
                new Edge<int>(5,6),
                new Edge<int>(7,8),
                new Edge<int>(9,10),
                new Edge<int>(12,13),
                new Edge<int>(13,14),
                new Edge<int>(14,15),
                new Edge<int>(14,17),
                new Edge<int>(17,18),
                new Edge<int>(18,16),
                new Edge<int>(15,16),
            });

            return forest;
        }

        private bool IsPathCorrect(List<Edge<int>> path, UndirectedGraph<int, Edge<int>> g, int source, int target)
        {
            foreach (var edge in path)
            {
                if (!g.ContainsEdge(edge))
                    throw new ArgumentException("Path is not part of the graph");
            }

            if (path.Count == 0 && source != target)
                return false;

            int currentSource = source;

            foreach (var edge in path)
            {
                try
                {
                    currentSource = EdmondsAlgorithm.GetTargetVertex(edge, currentSource);
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            if (currentSource == target)
                return true;
            else
                return false;
        }

        [TestMethod]
        public void ContractGraph_Graph1()
        {
            var g = GetGraph1();
            var blossom = new List<Edge<int>>
            {
                new Edge<int>(0,4),
                new Edge<int>(0,1),
                new Edge<int>(1,4),
            };
            var matching = new List<Edge<int>>
            {
                new Edge<int>(4, 1),
                new Edge<int>(2, 3),
            };

            var contractedGraph = EdmondsAlgorithm.ContractGraph(g, blossom, out int superVertex, matching);

            Assert.IsFalse(IsSuperVertexMatched(superVertex, matching));

            Assert.AreEqual(contractedGraph.EdgeCount, g.EdgeCount - blossom.Count);
        }

        [TestMethod]
        public void ContractGraph_Graph2_test1()
        {
            var g = GetGraph2();
            var blossom = new List<Edge<int>>
            {
                new Edge<int>(1,2),
                new Edge<int>(2,4),
                new Edge<int>(1,4),
            };
            var matching = new List<Edge<int>>
            {
                new Edge<int>(4, 1),
                new Edge<int>(0, 3),
            };

            var contractedGraph = EdmondsAlgorithm.ContractGraph(g, blossom, out int superVertex, matching);

            Assert.IsFalse(IsSuperVertexMatched(superVertex, matching));

            Assert.IsTrue(IsContractedGraphCorrect(contractedGraph, blossom, superVertex));
        }

        [TestMethod]
        public void ContractGraph_Graph2_test2()
        {
            var g = GetGraph2();
            var blossom = new List<Edge<int>>
            {
                new Edge<int>(1,2),
                new Edge<int>(2,4),
                new Edge<int>(3,4),
                new Edge<int>(3,0),
                new Edge<int>(0,1),
            };
            var matching = new List<Edge<int>>
            {
                new Edge<int>(0, 1),
                new Edge<int>(4, 3),
                new Edge<int>(8, 7),
            };

            var contractedGraph = EdmondsAlgorithm.ContractGraph(g, blossom, out int superVertex, matching);

            Assert.IsFalse(IsSuperVertexMatched(superVertex, matching));

            Assert.IsTrue(IsContractedGraphCorrect(contractedGraph, blossom, superVertex));

        }

        private bool IsSuperVertexMatched(int superVertex, List<Edge<int>> matching)
        {
            bool isSuperVertexMatched = false;
            foreach (var edge in matching)
            {
                if (edge.Source == superVertex || edge.Target == superVertex)
                {
                    isSuperVertexMatched = true;
                    break;
                }
            }
            return isSuperVertexMatched;
        }

        private bool IsContractedGraphCorrect(UndirectedGraph<int, Edge<int>> contractedGraph, List<Edge<int>> blossom, int superVertex)
        {
            var blossomVertices = new HashSet<int>();
            foreach (var edge in blossom)
            {
                blossomVertices.Add(edge.Source);
                blossomVertices.Add(edge.Target);
            }

            foreach (var edge in contractedGraph.Edges)
            {
                if (blossomVertices.Contains(edge.Source) && edge.Source != superVertex)
                    return false;
                if (blossomVertices.Contains(edge.Target) && edge.Target != superVertex)
                    return false;
            }
            return true;
        }

        private static AdvancedAlgorithms.TestResult VerifyLiftedAugmentingPath(
            UndirectedGraph<int, Edge<int>> g, List<Edge<int>> liftedAugmentingPath, List<Edge<int>> expectedAugmentingPath)
        {
            bool areEdgesInOriginalGraph = AreEdgesInOriginalGraph(g, liftedAugmentingPath);
            if (!areEdgesInOriginalGraph)
                return new AdvancedAlgorithms.TestResult(false, "Edges in lifted path do not belong to the graph");
            //Assert.IsTrue(areEdgesInOriginalGraph, "Edges in lifted path do not belong to the graph");

            if (1 != liftedAugmentingPath.Count % 2)
                return new AdvancedAlgorithms.TestResult(false, "Lifted path has even number of edges");
            //Assert.AreEqual(1, liftedAugmentingPath.Count % 2, "Lifted path has even number of edges");

            var pathComparer = new PathComparer();
            if (!pathComparer.Equals(expectedAugmentingPath, liftedAugmentingPath))
                return new AdvancedAlgorithms.TestResult(false, "Paths do not match");
            //Assert.IsTrue(pathComparer.Equals(expectedAugmentingPath, liftedAugmentingPath), "Paths do not match");
            return new AdvancedAlgorithms.TestResult(true, "");
        }

        private static bool AreEdgesInOriginalGraph(UndirectedGraph<int, Edge<int>> g, List<Edge<int>> liftedAugmentingPath)
        {
            var edgesSet = new HashSet<Edge<int>>(g.Edges, new EdgeComparer());
            bool areEdgesInOriginalGraph = true;
            foreach (var edge in liftedAugmentingPath)
            {
                if (!edgesSet.Contains(edge))
                {
                    areEdgesInOriginalGraph = false;
                    break;
                }
            }

            return areEdgesInOriginalGraph;
        }

        [TestMethod]
        public void GetPathEnds_ShouldReturn_0_1()
        {
            var path = new List<Edge<int>>
            {
                new Edge<int>(5,0),
                new Edge<int>(3,5),
                new Edge<int>(3,8),
                new Edge<int>(9,8),
                new Edge<int>(9,4),
                new Edge<int>(4,2),
                new Edge<int>(2,1),
            };

            EdmondsAlgorithm.GetPathEnds(path, out int beginning, out int end);

            Assert.AreEqual(0, beginning);
            Assert.AreEqual(1, end);
        }

        [TestMethod]
        public void GetPathEnds_ShouldReturn_1_0()
        {
            var path = new List<Edge<int>>
            {
                new Edge<int>(2,1),
                new Edge<int>(4,2),
                new Edge<int>(9,4),
                new Edge<int>(9,8),
                new Edge<int>(3,8),
                new Edge<int>(3,5),
                new Edge<int>(5,0),
            };

            EdmondsAlgorithm.GetPathEnds(path, out int beginning, out int end);

            Assert.AreEqual(1, beginning);
            Assert.AreEqual(0, end);
        }

        [TestMethod]
        public void RemoveEdgeFromGraph_ShouldRemove()
        {
            var g = new UndirectedGraph<int, Edge<int>>();

            var edges = GetGraph1().Edges;
            g.AddVerticesAndEdgeRange(edges);

            foreach (var edge in edges)
            {
                int originalGraphCount = g.EdgeCount;
                bool edgeRemoved = EdmondsAlgorithm.RemoveEdgeFromGraph(g, edge, out UndirectedGraph<int, Edge<int>> smallerGraph);
                g = smallerGraph;

                Assert.IsTrue(edgeRemoved, "Edge was not removed");
                int edgesCountDifference = originalGraphCount - smallerGraph.EdgeCount;
                Assert.AreEqual(1, edgesCountDifference, $"Edges numbers are incorrect. " +
                    $"Expected <{originalGraphCount}>. Actual <{smallerGraph.EdgeCount}>");
            }
        }
    }

}
