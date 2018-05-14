using ASD.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms
{
    public static class FileParser
    {
        public static bool TryParseLine(string line, out Graph g)
        {
            g = new AdjacencyListsGraph<AVLAdjacencyList>(true, 1); // for test purposes
            return true;
        }
    }
}
