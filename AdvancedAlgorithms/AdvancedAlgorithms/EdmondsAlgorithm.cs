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
            return FindMaximumMatching(g, initialMatching);
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
            connectingTreesEdge = null;
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
                                var pathStack = new Stack<Edge<int>>();
                                bool pathFound = DFSSearch(v, w, visited, g, pathStack);
                                if (!pathFound)
                                    throw new ArgumentException();
                                var pathFromVToW = new List<Edge<int>>();
                                while (pathStack.Count != 0)
                                {
                                    pathFromVToW.Add(pathStack.Pop());
                                }

                                var blossom = new List<Edge<int>>(pathFromVToW);
                                blossom.Add(edgeVW);

                                var contractedMatching = ContractMatching(currentMatching, blossom);
                                var contractedGraph = ContractGraph(g, blossom, out int superVertex, currentMatching);

                                var contractedAugmentingPath = FindAugmentingPath(contractedGraph, contractedMatching, out Edge<int> edgeBetweenTrees);
                                if (contractedAugmentingPath.Count == 0)
                                    return contractedAugmentingPath;

                                return LiftAugmentingPath(contractedAugmentingPath, blossom, g, edgeBetweenTrees, superVertex);
                            }
                        }
                    }
                    usedEdges.Add(edgeVW);
                }
                verticesInF.Remove(v);
                verticesUsed[v] = true;
            }

            return new List<Edge<int>>();
        }

        /// <summary>
        /// </summary>
        /// <param name="augmentingPath"></param>
        /// <param name="blossom"></param>
        /// <returns></returns>
        // TODO: Debug through it
        private static List<Edge<int>> LiftAugmentingPath(List<Edge<int>> augmentingPath, List<Edge<int>> blossom, UndirectedGraph<int, Edge<int>> g, Edge<int> edgeBetweenTrees, int superVertex)
        {
            var liftedAugmentingPath = new List<Edge<int>>(augmentingPath);
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

            int edgeBetweenVertexInFirstTree = edgeBetweenTrees.Source;
            if (edgeBetweenVertexInFirstTree == superVertex)
                edgeBetweenVertexInFirstTree = edgeBetweenTrees.Target;

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
            int edgeBetweenVertexInSecondTree = edgeBetweenInFullGraph.Target;
            if (edgeBetweenVertexInSecondTree == edgeBetweenVertexInFirstTree)
                edgeBetweenVertexInSecondTree = edgeBetweenInFullGraph.Source;

            var pathFromBlossom = new List<Edge<int>>(blossom);
            foreach (var edge in blossom)
            {
                if (edge.Source == edgeBetweenVertexInSecondTree && edge.Target == superVertex ||
                    edge.Target == edgeBetweenVertexInSecondTree && edge.Source == superVertex)
                {
                    pathFromBlossom.Remove(edge);
                    break;
                }
            }
            //if (pathFromBlossom.Count == blossom.Count)
            //    throw new ArgumentException();

            foreach (var edge in pathInFirstTree)
            {
                liftedAugmentingPath.Add(edge);
            }
            augmentingPath.Add(edgeBetweenInFullGraph);
            foreach (var edge in pathFromBlossom)
            {
                augmentingPath.Add(edge);
            }
            foreach (var edge in pathInSecondTree)
            {
                augmentingPath.Add(edge);
            }



            return liftedAugmentingPath;
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
        public static bool DFSSearch(int source, int destination, Dictionary<int, bool> visited, UndirectedGraph<int, Edge<int>> g, Stack<Edge<int>> stack)
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
                destinationFound = DFSSearch(targerVertex, destination, visited, g, stack);
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
