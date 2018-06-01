using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace AdvancedAlgorithms
{
    public class PathComparer : IEqualityComparer<List<Edge<int>>>
    {
        public bool Equals(List<Edge<int>> path1, List<Edge<int>> path2)
        {
            if (path1.Count != path2.Count)
                return false;
            var path1Set = new HashSet<Edge<int>>(path1, new EdgeComparer());
            foreach (var edge in path2)
            {
                if (!path1Set.Contains(edge))
                    return false;
            }

            var path2Set = new HashSet<Edge<int>>(path2, new EdgeComparer());
            foreach (var edge in path1)
            {
                if (!path2Set.Contains(edge))
                    return false;
            }

            return true;
        }

        public int GetHashCode(List<Edge<int>> obj)
        {
            throw new NotImplementedException();
        }
    }
}
