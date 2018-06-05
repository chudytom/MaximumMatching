using System;
using System.Collections.Generic;
using AdvancedAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace AdvancedAlgorithmsTests
{
    [TestClass]
    public class GraphGeneratorTests
    {
        private int ExpectedEdgesCountInFullGraph(int verticesCount) => (verticesCount * (verticesCount - 1)) / 2;

        [TestMethod]
        public void GetFullGraph_CountTest_ShouldReturn_1()
        {
            int verticesCount = 2;
            var generator = new GraphGenerator();
            var g = generator.GetFullGraph(verticesCount);

            Assert.AreEqual(ExpectedEdgesCountInFullGraph(verticesCount), g.EdgeCount);
        }

        [TestMethod]
        public void GetFullGraph_CountTest_ShouldReturn_3()
        {
            int verticesCount = 3;
            var generator = new GraphGenerator();
            var g = generator.GetFullGraph(verticesCount);

            Assert.AreEqual(ExpectedEdgesCountInFullGraph(verticesCount), g.EdgeCount);
        }

        [TestMethod]
        public void GetFullGraph_CountTest_ShouldReturn_6()
        {
            int verticesCount = 4;
            var generator = new GraphGenerator();
            var g = generator.GetFullGraph(verticesCount);

            Assert.AreEqual(ExpectedEdgesCountInFullGraph(verticesCount), g.EdgeCount);
        }

        [TestMethod]
        public void GetFullGraph_CountTest_ShouldReturn_45()
        {
            int verticesCount = 10;
            var generator = new GraphGenerator();
            var g = generator.GetFullGraph(verticesCount);

            Assert.AreEqual(ExpectedEdgesCountInFullGraph(verticesCount), g.EdgeCount);
        }

        [TestMethod]
        public void GetCycleGraph_CountTest_ShouldRetun_2()
        {
            int verticesCount = 2;
            var generator = new GraphGenerator();
            var g = generator.GetCycleGraph(verticesCount);

            Assert.AreEqual(verticesCount, g.EdgeCount);

            foreach (var source in g.Vertices)
            {
                foreach (var target in g.Vertices)
                {
                    Assert.IsTrue(EdmondsAlgorithm.DFSSearch(source, target, g, out List<Edge<int>> path));
                }
            }
        }

        [TestMethod]
        public void GetCycleGraph_CountTest_ShouldRetun_3()
        {
            int verticesCount = 3;
            var generator = new GraphGenerator();
            var g = generator.GetCycleGraph(verticesCount);

            Assert.AreEqual(verticesCount, g.EdgeCount);

            foreach (var source in g.Vertices)
            {
                foreach (var target in g.Vertices)
                {
                    Assert.IsTrue(EdmondsAlgorithm.DFSSearch(source, target, g, out List<Edge<int>> path));
                }
            }
        }

        [TestMethod]
        public void GetCycleGraph_CountTest_ShouldRetun_10()
        {
            int verticesCount = 10;
            var generator = new GraphGenerator();
            var g = generator.GetCycleGraph(verticesCount);

            Assert.AreEqual(verticesCount, g.EdgeCount);

            foreach (var source in g.Vertices)
            {
                foreach (var target in g.Vertices)
                {
                    Assert.IsTrue(EdmondsAlgorithm.DFSSearch(source, target, g, out List<Edge<int>> path));
                }
            }
        }

        [TestMethod]
        public void GetCycleGraph_CountTest_ShouldRetun_100()
        {
            int verticesCount = 100;
            var generator = new GraphGenerator();
            var g = generator.GetCycleGraph(verticesCount);

            Assert.AreEqual(verticesCount, g.EdgeCount);

            foreach (var source in g.Vertices)
            {
                foreach (var target in g.Vertices)
                {
                    Assert.IsTrue(EdmondsAlgorithm.DFSSearch(source, target, g, out List<Edge<int>> path));
                }
            }
        }



    }
}
