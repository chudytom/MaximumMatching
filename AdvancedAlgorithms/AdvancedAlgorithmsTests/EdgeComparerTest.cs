using System;
using AdvancedAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace AdvancedAlgorithmsTests
{
    [TestClass]
    public class EdgeComparerTest
    {
        [TestMethod]
        public void EdgeComparerEquals1_SameEdge_ShouldReturn_true()
        {
            throw new ArgumentException();
            var edge1 = new Edge<int>(0, 1);
            var comparer = new EdgeComparer();
            Assert.AreEqual(true, comparer.Equals(edge1, edge1));
        }

        [TestMethod]
        public void EdgeComparerEquals_SameEdges_ShouldReturn_true()
        {
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(0, 1);
            var comparer = new EdgeComparer();
            Assert.AreEqual(true, comparer.Equals(edge1, edge2));
        }

        [TestMethod]
        public void EdgeComparerEquals_SourceAndTargetExchanged_ShouldReturn_true()
        {
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(1, 0);
            var comparer = new EdgeComparer();
            Assert.AreEqual(true, comparer.Equals(edge1, edge2));
        }

        [TestMethod]
        public void EdgeComparerEquals1_ShouldReturn_false()
        {
            var edge1 = new Edge<int>(0, 0);
            var edge2 = new Edge<int>(1, 1);
            var comparer = new EdgeComparer();
            Assert.AreEqual(false, comparer.Equals(edge1, edge2));
        }

        [TestMethod]
        public void EdgeComparerEquals2_ShouldReturn_false()
        {
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(1, 1);
            var comparer = new EdgeComparer();
            Assert.AreEqual(false, comparer.Equals(edge1, edge2));
        }

        [TestMethod]
        public void EdgeComparerEquals3_ShouldReturn_false()
        {
            var edge1 = new Edge<int>(12, 1);
            var edge2 = new Edge<int>(6, 2);
            var comparer = new EdgeComparer();
            Assert.AreEqual(false, comparer.Equals(edge1, edge2));
        }

        [TestMethod]
        public void EdgeComparerGetHashCode1_ShouldReturn_true()
        {
            var edge1 = new Edge<int>(12, 1);
            var edge2 = new Edge<int>(1, 12);
            var comparer = new EdgeComparer();
            Assert.AreEqual(comparer.GetHashCode(edge1), comparer.GetHashCode(edge2));
        }

        [TestMethod]
        public void EdgeComparerGetHashCode2_ShouldReturn_true()
        {
            var edge1 = new Edge<int>(1, 13);
            var edge2 = new Edge<int>(1, 13);
            var comparer = new EdgeComparer();
            Assert.AreEqual(comparer.GetHashCode(edge1), comparer.GetHashCode(edge2));
        }

        [TestMethod]
        public void EdgeComparerGetHashCode3_ShouldReturn_false()
        {
            var edge1 = new Edge<int>(1, 12);
            var edge2 = new Edge<int>(2, 6);
            var comparer = new EdgeComparer();
            Assert.AreNotEqual(comparer.GetHashCode(edge1), comparer.GetHashCode(edge2));
        }


    }
}
