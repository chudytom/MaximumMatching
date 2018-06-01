using System;
using AdvancedAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
