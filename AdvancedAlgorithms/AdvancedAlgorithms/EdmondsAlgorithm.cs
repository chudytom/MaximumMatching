using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedAlgorithms
{
    public static class EdmondsAlgorithm
    {
        // Test written, algorithm needs improvement
        public static List<Edge<int>> CalculateMaximumMatching(UndirectedGraph<int, Edge<int>> g)
        {
            var initialMatching = new List<Edge<int>>();
            var result = FindMaximumMatching(g, initialMatching);
            var badEdges = new List<Edge<int>>();
            if (!CheckResult(result, ref badEdges))
            {
                Console.WriteLine("BAD MAXIMUM MATCHING !!!");
                // maybe we should remove bad edges and return maximum matching without them ???
                foreach (var edge in badEdges)
                {
                    result.Remove(edge);
                }
                Console.WriteLine("Removed {0} bad edges from maximum matching", badEdges.Count);

            }
            return result;
        }

        private static bool CheckResult(List<Edge<int>> edgesList, ref List<Edge<int>> badEdges)
        {
            List<int> vertices = new List<int>();
            int counter = 0;
            foreach (var edge in edgesList)
            {
                if (vertices.Contains(edge.Source) || vertices.Contains(edge.Target))
                {
                    badEdges.Add(edge);
                }
                counter++;
                vertices.Add(edge.Source);
                vertices.Add(edge.Target);
            }
            if (badEdges.Count > 0)
            {
                return false;
            }
            return true;
        }

        private static List<Edge<int>> FindMaximumMatching(UndirectedGraph<int, Edge<int>> G, List<Edge<int>> M)
        {
            var augmentingPath = FindAugmentingPath(G, M, out Edge<int> connectingTreesEdge);
            if (augmentingPath.Count != 0)
                return FindMaximumMatching(G, GetSymmetricalDifference(M, augmentingPath));
            else
                return M;
        }

        // Tested
        public static List<Edge<int>> GetSymmetricalDifference(List<Edge<int>> collection1, List<Edge<int>> collection2)
        {
            var set1 = new HashSet<Edge<int>>(collection1, new EdgeComparer());
            var set2 = new HashSet<Edge<int>>(collection2, new EdgeComparer());

            var difference = new HashSet<Edge<int>>(new EdgeComparer());

            foreach (var edge in collection1)
            {
                if (!set2.Contains(edge))
                    difference.Add(edge);
            }

            foreach (var edge in collection2)
            {
                if (!set1.Contains(edge))
                    difference.Add(edge);
            }

            return new List<Edge<int>>(difference);
        }

        /// <summary>
        /// </summary>
        /// <param name="g"></param>
        /// <param name="currentMatching"></param>
        /// <returns></returns>
        private static List<Edge<int>> FindAugmentingPath(UndirectedGraph<int, Edge<int>> g, List<Edge<int>> currentMatching, out Edge<int> connectingTreesEdge)
        {
            //connectingTreesEdge = null;
            var F = new UndirectedGraph<int, Edge<int>>(); //Forest F
            HashSet<Edge<int>> usedEdges = new HashSet<Edge<int>>(new EdgeComparer());
            var vertices = new List<int>(g.Vertices).ToArray();
            var edges = new List<Edge<int>>(g.Edges).ToArray();
            bool[] verticesUsed = new bool[g.VertexCount];
            int[] verticesLevels = new int[g.VertexCount];
            for (int i = 0; i < verticesLevels.Length; i++)
            {
                verticesLevels[i] = -1;
            }

            if (vertices.Length != verticesUsed.Length)
                throw new InvalidOperationException();
            var isMatched = new bool[g.VertexCount];
            foreach (var edge in currentMatching)
            {
                isMatched[edge.Source] = true;
                isMatched[edge.Target] = true;
                usedEdges.Add(edge);
            }
            for (int i = 0; i < isMatched.Length; i++)
            {
                if (isMatched[i] == false)
                {
                    F.AddVertex(i);
                    verticesLevels[i] = 0;
                }
            }

            var verticesInF = new HashSet<int>(F.Vertices);

            while (verticesInF.Count != 0)
            {
                int v = verticesInF.First();
                foreach (var tempVertex in F.Vertices)
                {
                    if (verticesUsed[tempVertex] == false)
                        verticesInF.Add(tempVertex);
                }
                if (verticesUsed[v])
                    continue;
                verticesUsed[v] = true;
                verticesInF.Remove(v);

                if (verticesLevels[v] != -1 && verticesLevels[v] % 2 == 1)
                    continue;
                foreach (var edgeVW in g.AdjacentEdges(v))
                {
                    if (usedEdges.Contains(edgeVW))
                        continue;
                    int w = edgeVW.Target;
                    if (w == v)
                        w = edgeVW.Source;
                    if (!F.ContainsVertex(w))
                    {
                        foreach (var matchedEdge in currentMatching)
                        {
                            if (!(matchedEdge.Source == w || matchedEdge.Target == w))
                                continue;
                            int x = GetTargetVertex(matchedEdge, w);
                            if (x == -1)
                                throw new ArgumentException();
                            verticesLevels[w] = verticesLevels[v] + 1;
                            verticesLevels[x] = verticesLevels[w] + 1;
                            F.AddVerticesAndEdge(edgeVW);
                            F.AddVerticesAndEdge(matchedEdge);
                        }
                        if (F.EdgeCount == 0)
                            throw new ArgumentException();
                    }
                    else
                    {
                        //connectingTreesEdge = edgeVW;
                        if (verticesLevels[w] == -1)
                            throw new ArgumentException();
                        if (verticesLevels[w] % 2 == 1)
                        {
                            //In this case we do nothing
                        }
                        else
                        {
                            bool vHasRoot = TryGetRootForVertex(F, v, verticesLevels, out int rootForV, out List<Edge<int>> pathToVRoot);
                            bool wHasRoot = TryGetRootForVertex(F, w, verticesLevels, out int rootForW, out List<Edge<int>> pathToWRoot);
                            if (!vHasRoot || !wHasRoot)
                                throw new ArgumentException();
                            if (rootForV != rootForW)
                            {
                                Stack<Edge<int>> vRootPath = new Stack<Edge<int>>(pathToVRoot);
                                Queue<Edge<int>> wRootPath = new Queue<Edge<int>>(pathToWRoot);
                                var augmentingPath = new List<Edge<int>>();

                                while (vRootPath.Count != 0)
                                {
                                    augmentingPath.Add(vRootPath.Pop());
                                }
                                augmentingPath.Add(edgeVW);
                                while (wRootPath.Count != 0)
                                {
                                    augmentingPath.Add(wRootPath.Dequeue());
                                }
                                connectingTreesEdge = edgeVW;
                                if (augmentingPath.Count % 2 == 0)
                                    throw new ArgumentException();
                                return augmentingPath;
                            }
                            else
                            {
                                Stack<int> stack = new Stack<int>();
                                var visited = new Dictionary<int, bool>();
                                foreach (var vertex in F.Vertices)
                                {
                                    visited.Add(vertex, false);
                                }
                                //var blossom = new Stack<Edge<int>>();
                                bool pathFound = DFSSearch(v, w, F, out List<Edge<int>> blossom);
                                if (!pathFound)
                                    throw new ArgumentException();

                                blossom.Add(edgeVW);
                                TestResult blossomCorrectResult = VerifyBlossom(blossom, currentMatching);
                                if (!blossomCorrectResult.IsCorrect)
                                    throw new ArgumentException(blossomCorrectResult.ErrorMessage);

                                var contractedMatching = ContractMatching(currentMatching, blossom);
                                var contractedGraph = ContractGraph(g, blossom, out int superVertex, currentMatching);

                                var contractedAugmentingPath = FindAugmentingPath(contractedGraph, contractedMatching, out Edge<int> edgeBetweenTrees);
                                if (contractedAugmentingPath.Count == 0)
                                {
                                    connectingTreesEdge = null;
                                    return contractedAugmentingPath;
                                }

                                // LiftAugmentingPath should return some edge
                                var liftedAugmentingPath = LiftAugmentingPath(contractedAugmentingPath, blossom, g, edgeBetweenTrees, superVertex, currentMatching);
                                if (liftedAugmentingPath.Count % 2 == 0)
                                    throw new ArgumentException();
                                //connectingTreesEdge = liftedEdgeBetweenTrees;
                                connectingTreesEdge = edgeVW;
                                return liftedAugmentingPath;
                            }
                        }
                    }
                    usedEdges.Add(edgeVW);
                }
                verticesInF.Remove(v);
                verticesUsed[v] = true;
            }
            connectingTreesEdge = null;
            return new List<Edge<int>>();
        }

        private static TestResult VerifyBlossom(List<Edge<int>> blossom, List<Edge<int>> matching)
        {

            if (blossom.Count % 2 == 0)
                return new TestResult(false, "The cycle has an even number of edges");
            var matchedVerticesInGraph = GetAllVerticesForPath(matching);
            var matchedVerticesInBlossom = new HashSet<int>();
            var unmatchedVerticesInBlossom = new HashSet<int>();
            foreach (var edge in blossom)
            {
                if (matchedVerticesInGraph.Contains(edge.Source))
                    matchedVerticesInBlossom.Add(edge.Source);
                else
                    unmatchedVerticesInBlossom.Add(edge.Source);
                if (matchedVerticesInGraph.Contains(edge.Target))
                    matchedVerticesInBlossom.Add(edge.Target);
                else
                    unmatchedVerticesInBlossom.Add(edge.Target);
            }
            if (unmatchedVerticesInBlossom.Count != 1)
                return new TestResult(false, "There should be one unmatched vertex in blossom" +
                    $"Expected: <{1}>. Actual <{unmatchedVerticesInBlossom.Count}>");
            if (matchedVerticesInBlossom.Count != (blossom.Count - 1))
                return new TestResult(false, "There should be all matched vertices in blossom except one" +
                    $"Expected: <{blossom.Count - 1}>. Actual <{matchedVerticesInBlossom.Count}>");
            return new TestResult(true, "");
        }

        public static HashSet<int> GetAllVerticesForPath(List<Edge<int>> path)
        {
            var allVertices = new HashSet<int>();
            foreach (var edge in path)
            {
                allVertices.Add(edge.Source);
                allVertices.Add(edge.Target);
            }
            return allVertices;
        }

        /// <summary>
        /// </summary>
        /// <param name="augmentingPath"></param>
        /// <param name="blossom"></param>
        /// <returns></returns>
        // TODO: Debug through it
        public static List<Edge<int>> LiftAugmentingPath(List<Edge<int>> augmentingPath, List<Edge<int>> blossom, UndirectedGraph<int, Edge<int>> g, Edge<int> edgeBetweenTrees, int superVertex, List<Edge<int>> currentMatching)
        {

            //GetPathEnds(augmentingPath, out int pathBeginnign, out int pathEnd);
            //if (augmentingPath.Count == 1)
            //return LiftingV1(augmentingPath, blossom, g, edgeBetweenTrees, superVertex, currentMatching);
            //else
            //return LiftingV2(augmentingPath, blossom, g, edgeBetweenTrees, superVertex, currentMatching);

            return LiftingV3(augmentingPath, blossom, g, edgeBetweenTrees, superVertex, currentMatching);
        }

        public static void GetPathEnds(List<Edge<int>> augmentingPath, out int beginning, out int end)
        {
            if (augmentingPath.Count == 0)
                throw new ArgumentException();
            int previousVertex = -1, firstVertex, lastVertex;
            var path = new Queue<Edge<int>>(augmentingPath);
            var firstEdge = path.Dequeue();

            firstVertex = firstEdge.Source;
            lastVertex = firstEdge.Target;
            if (path.Count > 0)
            {
                var secondEdge = path.Peek();
                if (firstVertex == secondEdge.Source || firstVertex == secondEdge.Target)
                {
                    previousVertex = firstVertex;
                    firstVertex = lastVertex;
                }
                else
                {
                    previousVertex = lastVertex;
                }
                while (path.Count != 0)
                {
                    var edge = path.Dequeue();
                    if (edge.Source == previousVertex)
                        previousVertex = edge.Target;
                    else if (edge.Target == previousVertex)
                        previousVertex = edge.Source;
                    else
                        throw new ArgumentException("Incorrect path");
                }
                lastVertex = previousVertex;
            }
            beginning = firstVertex;
            end = lastVertex;
        }

        private static List<Edge<int>> LiftingV1(List<Edge<int>> augmentingPath, List<Edge<int>> blossom, UndirectedGraph<int, Edge<int>> g, Edge<int> edgeBetweenTrees, int superVertex, List<Edge<int>> currentMatching)
        {
            return augmentingPath;
        }

        private static List<Edge<int>> LiftingV2(List<Edge<int>> augmentingPath, List<Edge<int>> blossom, UndirectedGraph<int, Edge<int>> g, Edge<int> edgeBetweenTrees, int superVertex, List<Edge<int>> currentMatching)
        {
            var liftedAugmentingPath = new List<Edge<int>>();
            var blossomVertices = new HashSet<int>();
            foreach (var edge in blossom)
            {
                blossomVertices.Add(edge.Source);
                blossomVertices.Add(edge.Target);
            }

            var pathInFirstTree = new List<Edge<int>>();
            var pathInSecondTree = new List<Edge<int>>();
            bool isFirstTree = true;
            foreach (var edge in augmentingPath)
            {
                if (new EdgeComparer().Equals(edge, edgeBetweenTrees))
                {
                    isFirstTree = false;
                    continue;
                }
                if (isFirstTree)
                    pathInFirstTree.Add(edge);
                else
                    pathInSecondTree.Add(edge);
            }

            int edgeBetweenVertexInFirstTree = GetTargetVertex(edgeBetweenTrees, superVertex);

            Edge<int> edgeBetweenInFullGraph = null;

            foreach (var edge in g.AdjacentEdges(edgeBetweenVertexInFirstTree))
            {
                int targetVertex = GetTargetVertex(edge, edgeBetweenVertexInFirstTree);
                if (blossomVertices.Contains(targetVertex))
                {
                    edgeBetweenInFullGraph = edge;
                    break;
                }
            }

            if (edgeBetweenInFullGraph == null)
                throw new ArgumentException();
            int edgeBetweenVertexInSecondTree = GetTargetVertex(edgeBetweenInFullGraph, edgeBetweenVertexInFirstTree);

            var pathFromBlossom = new List<Edge<int>>(blossom);
            //Edge<int> edgeToRemove = null; 
            foreach (var edge in blossom)
            {
                if (edge.Source == edgeBetweenVertexInSecondTree && edge.Target == superVertex ||
                    edge.Target == edgeBetweenVertexInSecondTree && edge.Source == superVertex)
                {
                    pathFromBlossom.Remove(edge);
                    //edgeToRemove = edge;
                    break;
                }
            }

            var blossomGraph = new UndirectedGraph<int, Edge<int>>();
            blossomGraph.AddVertexRange(g.Vertices);
            foreach (var edge in pathFromBlossom)
            {
                blossomGraph.AddEdge(edge);
            }
            var pathStack = new Stack<Edge<int>>();
            pathFromBlossom.Clear();

            DFSSearch(edgeBetweenVertexInSecondTree, superVertex, blossomGraph, out pathFromBlossom);
            while (pathStack.Count != 0)
            {
                pathFromBlossom.Add(pathStack.Pop());
            }

            //if (pathFromBlossom.Count == blossom.Count)
            //    throw new ArgumentException();

            foreach (var edge in pathInFirstTree)
            {
                liftedAugmentingPath.Add(edge);
            }
            liftedAugmentingPath.Add(edgeBetweenInFullGraph);
            foreach (var edge in pathFromBlossom)
            {
                liftedAugmentingPath.Add(edge);
            }
            foreach (var edge in pathInSecondTree)
            {
                liftedAugmentingPath.Add(edge);
            }

            return liftedAugmentingPath;
        }

        private static List<Edge<int>> LiftingV3(List<Edge<int>> augmentingPath, List<Edge<int>> blossom, UndirectedGraph<int, Edge<int>> g, Edge<int> edgeBetweenTrees, int superVertex, List<Edge<int>> currentMatching)
        {
            var edgeComparer = new EdgeComparer();
            var matchedVertices = GetAllVerticesForPath(currentMatching);
            var matchedEdges = new HashSet<Edge<int>>(currentMatching, new EdgeComparer());
            GetPathEnds(augmentingPath, out int firstVertex, out int lastVertex);
            var superVertexEdges = new List<Edge<int>>();
            foreach (var edge in augmentingPath)
            {
                if (edge.Source == superVertex || edge.Target == superVertex)
                    superVertexEdges.Add(edge);
            }
            if (superVertexEdges.Count > 2)
                throw new ArgumentException("There can be max 2 edges adjacent to superVertex in augmenting path");
            var superVertexUnmatchedEdge = superVertexEdges.FirstOrDefault(edge => !matchedEdges.Contains(edge));
            var superVertexMatchedEdge = superVertexEdges.FirstOrDefault(edge => matchedEdges.Contains(edge));
            if (superVertexEdges.Count == 2 && (superVertexMatchedEdge == null || superVertexUnmatchedEdge == null))
                throw new ArgumentException("Super vertex edges found incorrectly");
            if (superVertexEdges.Count == 1 && (superVertexMatchedEdge == null && superVertexUnmatchedEdge == null))
                throw new ArgumentException("Super vertex edges found incorrectly");

            var pathForMatchedEdge = new List<Edge<int>>();
            var pathForUnmatchedEdge = new List<Edge<int>>();
            var pathForBlossom = new List<Edge<int>>();
            if (superVertexMatchedEdge != null)
            {
                int oppositeToSuperVertex1 = GetTargetVertex(superVertexMatchedEdge, superVertex);
                if (oppositeToSuperVertex1 != firstVertex && oppositeToSuperVertex1 != lastVertex)
                {
                    var augmentingPathGraph1 = GetGraphFromEdges(augmentingPath);
                    var edges = new HashSet<Edge<int>>(g.Edges, edgeComparer);
                    if (!edges.Contains(superVertexMatchedEdge))
                        throw new ArgumentException("Graph should contain matched Edge");
                    bool wasRemoved = RemoveEdgeFromGraph(augmentingPathGraph1, superVertexMatchedEdge, out augmentingPathGraph1);
                    //bool remove1 = augmentingPathGraph1.RemoveEdge(superVertexMatchedEdge);
                    //bool remove2 = augmentingPathGraph1.RemoveEdge(new Edge<int>(superVertexMatchedEdge.Target, superVertexMatchedEdge.Source));
                    if (!wasRemoved)
                        throw new ArgumentException("Edge was not removed");
                    bool pathFound = DFSSearch(firstVertex, oppositeToSuperVertex1, augmentingPathGraph1, out List<Edge<int>> path1);
                    if (pathFound)
                    {
                        pathForMatchedEdge = path1;
                    }
                    else
                    {
                        pathFound = DFSSearch(oppositeToSuperVertex1, lastVertex, augmentingPathGraph1, out List<Edge<int>> path2);
                        if (!pathFound)
                            throw new ArgumentException("Path should exist");
                        pathForMatchedEdge = path2;
                    }
                }
                pathForMatchedEdge.Add(superVertexMatchedEdge);
            }

            if (superVertexUnmatchedEdge != null)
            {
                int oppositeToSuperVertex2 = GetTargetVertex(superVertexUnmatchedEdge, superVertex);
                if (oppositeToSuperVertex2 != firstVertex && oppositeToSuperVertex2 != lastVertex)
                {
                    var augmentingPathGraph2 = GetGraphFromEdges(augmentingPath);
                    var edges = new HashSet<Edge<int>>(g.Edges, edgeComparer);
                    //if (!edges.Contains(superVertexUnmatchedEdge))
                    //    throw new ArgumentException("Graph should contain matched Edge");
                    bool wasRemoved = RemoveEdgeFromGraph(augmentingPathGraph2, superVertexUnmatchedEdge, out augmentingPathGraph2);
                    //bool remove1 = augmentingPathGraph2.RemoveEdge(superVertexUnmatchedEdge);
                    //bool remove2 = augmentingPathGraph2.RemoveEdge(new Edge<int>(superVertexUnmatchedEdge.Target, superVertexUnmatchedEdge.Source));
                    if (!wasRemoved)
                        throw new ArgumentException("Edge was not removed");
                    bool pathFound = DFSSearch(firstVertex, oppositeToSuperVertex2, augmentingPathGraph2, out List<Edge<int>> path1);
                    if (pathFound)
                    {
                        pathForUnmatchedEdge = path1;
                    }
                    else
                    {
                        pathFound = DFSSearch(oppositeToSuperVertex2, lastVertex, augmentingPathGraph2, out List<Edge<int>> path2);
                        if (!pathFound)
                            throw new ArgumentException("Path should exist");
                        pathForUnmatchedEdge = path2;
                    }
                }
                //pathForUnmatchedEdge.Add(superVertexUnmatchedEdge);
            }

            Edge<int> firstEdgeInPath = null;
            var blossomVertices = GetAllVerticesForPath(blossom);
            int oppositeToSuperVertex = GetTargetVertex(superVertexUnmatchedEdge, superVertex);
            int firstVertexOnBlossom = -1;
            foreach (var edge in g.AdjacentEdges(oppositeToSuperVertex))
            {
                int target = GetTargetVertex(edge, oppositeToSuperVertex);
                if (blossomVertices.Contains(target))
                {
                    firstEdgeInPath = edge;
                    firstVertexOnBlossom = target;
                    break;
                }
            }
            if (firstEdgeInPath == null || firstVertexOnBlossom < 0)
                throw new ArgumentException("This path should be found");
            if (!matchedVertices.Contains(firstVertexOnBlossom))
            {
                return augmentingPath;
            }
            if (firstVertexOnBlossom == superVertex)
            {
                pathForBlossom = new List<Edge<int>> { firstEdgeInPath };
            }
            else
            {
                var blossomGraph = GetGraphFromEdges(blossom);
                foreach (var edge in blossomGraph.AdjacentEdges(firstVertexOnBlossom))
                {
                    int target = GetTargetVertex(edge, firstVertexOnBlossom);
                    if (!matchedVertices.Contains(target))
                    {
                        bool edgeRemoved = blossomGraph.RemoveEdge(edge);
                        if (!edgeRemoved)
                            throw new ArgumentException("Edge should be removed");
                        break;
                    }
                }

                bool pathFound = DFSSearch(firstVertexOnBlossom, superVertex, blossomGraph, out pathForBlossom);
                pathForBlossom.Add(firstEdgeInPath);
            }

            // Merge results for all the 3 paths
            var augmentingPathGraph = GetGraphFromEdges(pathForBlossom);
            if (superVertexMatchedEdge != null)
                augmentingPathGraph.AddVerticesAndEdgeRange(pathForMatchedEdge);
            if (superVertexUnmatchedEdge != null)
                augmentingPathGraph.AddVerticesAndEdgeRange(pathForUnmatchedEdge);

            if (!augmentingPathGraph.Vertices.Contains(firstVertex) || !augmentingPathGraph.Vertices.Contains(lastVertex))
                throw new ArgumentException("First and last vertex must be a part of the graph");
            var liftedAugmentingPathFound = DFSSearch(firstVertex, lastVertex, augmentingPathGraph, out List<Edge<int>> liftedAugmentingPath);
            if (!liftedAugmentingPathFound)
                throw new ArgumentException("Lifted augmenting path not found");
            return liftedAugmentingPath;
        }

        private static UndirectedGraph<int, Edge<int>> GetGraphFromEdges(IEnumerable<Edge<int>> edges)
        {
            //var edgeComparer = new EdgeComparer();
            var graphFromEdges = new UndirectedGraph<int, Edge<int>>();
            foreach (var edge in edges)
            {
                graphFromEdges.AddVerticesAndEdge(edge);
            }
            if (graphFromEdges.EdgeCount != edges.Count())
                throw new ArgumentException("AugmentingPathGraph creation error");
            return graphFromEdges;
        }

        public static bool RemoveEdgeFromGraph(UndirectedGraph<int, Edge<int>> originalGraph, Edge<int> edgeToRemove, out UndirectedGraph<int, Edge<int>> resultGraph)
        {
            var edgeComparer = new EdgeComparer();
            var edges = new HashSet<Edge<int>>(originalGraph.Edges, edgeComparer);
            bool isRemoved = edges.Remove(edgeToRemove);
            if(!isRemoved)
            {
                resultGraph = originalGraph;
                return false;
            }
            else
            {
                resultGraph = GetGraphFromEdges(edges);
                return true;
            }
        }

        // Tested
        public static UndirectedGraph<int, Edge<int>> ContractGraph(UndirectedGraph<int, Edge<int>> g, List<Edge<int>> blossom, out int superVertex, IEnumerable<Edge<int>> matching)
        {
            var gNew = new UndirectedGraph<int, Edge<int>>();
            foreach (var vertex in g.Vertices)
            {
                gNew.AddVertex(vertex);
            }

            var blossomVertices = new HashSet<int>();
            foreach (var edge in blossom)
            {
                blossomVertices.Add(edge.Source);
                blossomVertices.Add(edge.Target);
            }

            var matchedVertices = new HashSet<int>();

            foreach (var edge in matching)
            {
                matchedVertices.Add(edge.Source);
                matchedVertices.Add(edge.Target);
            }

            superVertex = blossomVertices.First(vertex => !matchedVertices.Contains(vertex));



            foreach (var edge in g.Edges)
            {
                var containsSource = blossomVertices.Contains(edge.Source);
                var containsTarget = blossomVertices.Contains(edge.Target);

                if (containsSource && containsTarget)
                {
                    // wszystko w blossomie
                    // nie przepisujemy
                }
                else if (containsSource || containsTarget)
                {
                    // ciezko bo na zewnatrz (nowa krawedz hehehe)
                    if (containsSource)
                    {
                        gNew.AddEdge(new Edge<int>(edge.Target, superVertex));
                    }
                    else
                    {
                        gNew.AddEdge(new Edge<int>(edge.Source, superVertex));
                    }
                }
                else
                {
                    // przepisujemy
                    gNew.AddEdge(edge);
                }
            }

            return gNew;
        }

        // Tested
        public static List<Edge<int>> ContractMatching(List<Edge<int>> matching, List<Edge<int>> blossom)
        {
            var contractedMatching = new List<Edge<int>>();
            var blossomHashSet = new HashSet<Edge<int>>(blossom, new EdgeComparer());
            foreach (var edge in matching)
            {
                if (blossomHashSet.Contains(edge))
                    continue;
                else
                    contractedMatching.Add(edge);
            }
            return contractedMatching;
        }

        // Tested
        public static bool DFSSearch(int source, int destination, UndirectedGraph<int, Edge<int>> g, out List<Edge<int>> path)
        {
            var visited = new Dictionary<int, bool>();
            foreach (var vertex in g.Vertices)
            {
                visited.Add(vertex, false);
            }
            var pathStack = new Stack<Edge<int>>();
            bool pathFound = DFSSearchRecurse(source, destination, visited, g, pathStack);

            path = new List<Edge<int>>();
            while (pathStack.Count != 0)
            {
                path.Add(pathStack.Pop());
            }

            return pathFound;
        }

        public static bool DFSSearchRecurse(int source, int destination, Dictionary<int, bool> visited, UndirectedGraph<int, Edge<int>> g, Stack<Edge<int>> stack)
        {
            if (source == destination)
                return true;
            visited[source] = true;
            bool destinationFound = false;
            foreach (var edge in g.AdjacentEdges(source))
            {
                int targerVertex = GetTargetVertex(edge, source);
                if (visited[targerVertex])
                    continue;
                if (targerVertex == destination)
                {
                    stack.Push(edge);
                    return true;
                }
                destinationFound = DFSSearchRecurse(targerVertex, destination, visited, g, stack);
                if (destinationFound)
                {
                    stack.Push(edge);
                    return true;
                }
            }
            return destinationFound;
        }

        // Tested
        public static bool TryGetRootForVertex(UndirectedGraph<int, Edge<int>> forest, int vertex,
            int[] verticesLevels, out int rootVertex, out List<Edge<int>> pathToRoot)
        {
            pathToRoot = new List<Edge<int>>();
            rootVertex = -1;
            if (forest.ContainsVertex(vertex) == false || verticesLevels[vertex] == -1)
            {
                return false;
            }

            while (verticesLevels[vertex] != 0)
            {
                int higherVertex = -1;
                foreach (var edge in forest.AdjacentEdges(vertex))
                {
                    int targetVertex = GetTargetVertex(edge, vertex);
                    if (verticesLevels[targetVertex] == -1)
                        continue;
                    if (verticesLevels[targetVertex] == verticesLevels[vertex] - 1)
                    {
                        higherVertex = targetVertex;
                        pathToRoot.Add(edge);
                        vertex = higherVertex;
                        break;
                    }
                }
                if (higherVertex == -1)
                    return false;
            }
            rootVertex = vertex;
            return true;
        }

        // Tested
        public static int GetTargetVertex(Edge<int> edge, int source)
        {
            if (edge.Source == source)
                return edge.Target;
            else if (edge.Target == source)
                return edge.Source;
            else
                throw new ArgumentException("Incorrect edge or source ");
        }
    }
}
