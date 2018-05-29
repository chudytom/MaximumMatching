using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace AdvancedAlgorithms
{
    public class EdgeComparer : IEqualityComparer<Edge<int>>
    {
        public bool Equals(Edge<int> e1, Edge<int> e2)
        {
            if (e1.Source == e2.Source && e1.Target == e2.Target)
                return true;
            if (e1.Source == e2.Target && e1.Target == e2.Source)
                return true;
            else return false;
        }

        public int GetHashCode(Edge<int> edge)
        {
            int hCode = 1000 * edge.Source * edge.Target + edge.Source + edge.Target;
            return hCode.GetHashCode();
        }
    }
}
