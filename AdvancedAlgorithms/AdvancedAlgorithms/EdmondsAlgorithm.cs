using QuickGraph;
using System;
using System.Collections.Generic;

namespace AdvancedAlgorithms
{
    public static class EdmondsAlgorithm
    {
        public static List<Edge<int>> CalculateMaximumMatching(UndirectedGraph<int, Edge<int>> g)
        {
            var initialMatching = new List<Edge<int>>();
            return FindMaximumMatching(g, initialMatching);
        }

        private static List<Edge<int>> FindMaximumMatching(UndirectedGraph<int, Edge<int>> G, List<Edge<int>> M)
        {
            var augmentingPath = FindAugmentingPath(G, M);
            if (augmentingPath.Count != 0)
                return FindMaximumMatching(G, GetSymetricalDifference(M, augmentingPath));
            else
                return M;
        }

        private static List<Edge<int>> GetSymetricalDifference(List<Edge<int>> colletion1, List<Edge<int>> collection2)
        {
            var firstSet = new HashSet<Edge<int>>(colletion1);

            foreach (var item in collection2)
            {
                bool added = firstSet.Add(item);
                if (!added)
                    firstSet.Remove(item);
            }

            return new List<Edge<int>>(firstSet);
        }

        /// <summary>
        /// TODO: Finish the end of th algorithm
        /// </summary>
        /// <param name="g"></param>
        /// <param name="currentMatching"></param>
        /// <returns></returns>
        private static List<Edge<int>> FindAugmentingPath(UndirectedGraph<int, Edge<int>> g, List<Edge<int>> currentMatching)
        {
            var F = new UndirectedGraph<int, Edge<int>>(); //Forest F
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
            }
            for (int i = 0; i < isMatched.Length; i++)
            {
                if (isMatched[i] == false)
                {
                    F.AddVertex(i);
                    verticesLevels[i] = 0;
                }
            }

            for (int v = 0; v < g.VertexCount; v++)
            {
                if (verticesUsed[v])
                    continue;
                if (verticesLevels[v] != -1 && verticesLevels[v] % 2 == 1)
                    continue;
                foreach (var edgeVW in g.AdjacentEdges(v))
                {
                    int w = edgeVW.Target;
                    if (w == v)
                        w = edgeVW.Source;
                    if (!F.ContainsVertex(edgeVW.Target))
                    {
                        foreach (var matchedEdge in currentMatching)
                        {
                            int x = -1;
                            if (matchedEdge.Source == w)
                                x = matchedEdge.Target;
                            if (matchedEdge.Target == w)
                                x = matchedEdge.Source;
                            if (x == -1)
                                throw new ArgumentException();
                            verticesLevels[w] = verticesLevels[v] + 1;
                            verticesLevels[x] = verticesLevels[w] + 1;
                            F.AddEdge(edgeVW);
                            F.AddEdge(matchedEdge);
                        }
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
                                return augmentingPath;
                            }
                            else
                            {
                                Stack<int> stack = new Stack<int>();
                                bool[] visited = new bool[F.VertexCount];
                                var pathStack = new Stack<Edge<int>>();
                                bool pathFound = DFSSearch(v, w, visited, g, pathStack);
                                if (!pathFound)
                                    throw new ArgumentException();
                                var pathFromVToW = new List<Edge<int>>();
                                while(pathStack.Count!=0)
                                {
                                    pathToVRoot.Add(pathStack.Pop());
                                }

                                var blossom = new List<Edge<int>>(pathFromVToW);
                                blossom.Add(edgeVW);

                                var contractedGraph = ContractGraph(g, blossom);
                                var contractedMatching = ContractMatching(currentMatching, blossom);

                                var contractedAugmentingPath = FindAugmentingPath(contractedGraph, contractedMatching);


                                return LiftAugmentingPath(contractedAugmentingPath, blossom);
                            }
                        }
                    }
                }
            }

            return new List<Edge<int>>();
        }

        /// <summary>
        /// TODO: Finish this function
        /// </summary>
        /// <param name="augmentingPath"></param>
        /// <param name="blossom"></param>
        /// <returns></returns>
        private static List<Edge<int>> LiftAugmentingPath(List<Edge<int>> augmentingPath, List<Edge<int>> blossom)
        {
            var liftedAugmentingPath = new List<Edge<int>>(augmentingPath);
            foreach (var edge in blossom)
            {
                
            }
            return liftedAugmentingPath;
        }

        private static UndirectedGraph<int, Edge<int>> ContractGraph(UndirectedGraph<int, Edge<int>> g, List<Edge<int>> blossom)
        {
            var contractedGraph = new UndirectedGraph<int, Edge<int>>();
            foreach (var edge in g.Edges)
            {
                if (blossom.Contains(edge))
                    continue;
                contractedGraph.AddEdge(edge);
            }

            return contractedGraph;
        }

        private static List<Edge<int>> ContractMatching(List<Edge<int>> matching, List<Edge<int>> blossom)
        {
            var contractedMatching = new List<Edge<int>>();
            var blossomStack = new HashSet<Edge<int>>();
            foreach (var edge in matching)
            {
                if (blossomStack.Contains(edge))
                    continue;
                else
                    contractedMatching.Add(edge);
            }
            return contractedMatching;
        }

        private static bool DFSSearch(int source, int destination, bool[] visited, UndirectedGraph<int, Edge<int>> g, Stack<Edge<int>> stack)
        {
            visited[source] = true;
            bool destinationFound = false;
            foreach (var edge in g.AdjacentEdges(source))
            {
                if (visited[edge.Target])
                    continue;
                if (edge.Target == destination)
                {
                    stack.Push(edge);
                    return true;
                }
                destinationFound = DFSSearch(edge.Target, destination, visited, g, stack);
                if (destinationFound)
                {
                    stack.Push(edge);
                    return true;
                }
            }
            return destinationFound;
        }

        private static bool TryGetRootForVertex(UndirectedGraph<int, Edge<int>> tree, int vertex,
            int[] verticesLevels, out int rootVertex, out List<Edge<int>> pathToRoot)
        {
            pathToRoot = new List<Edge<int>>();
            rootVertex = -1;
            if (verticesLevels[vertex] == -1 || tree.ContainsVertex(vertex) == false)
            {
                return false;
            }

            while (verticesLevels[vertex] != 0)
            {
                int higherVertex = -1;
                foreach (var edge in tree.AdjacentEdges(vertex))
                {
                    if (verticesLevels[edge.Target] == -1)
                        continue;
                    if (verticesLevels[edge.Target] == verticesLevels[vertex] + 1)
                    {
                        higherVertex = edge.Target;
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
    }
}
