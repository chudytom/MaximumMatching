using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AdvancedAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace AdvancedAlgorithmsTests
{
    [TestClass]
    public class FileParserTests
    {
        [TestMethod]
        public void ParseTest1ShouldReturnCorrectGraph()
        {
            UndirectedGraph<int, Edge<int>> g;
            List<Tuple<int, int>> pairs;
            string filePath = FindFilePath("test1.txt");
            var result = FileParser.TryParseFile(filePath, out g, out pairs);
            Assert.AreEqual(9, g.VertexCount);
            Assert.AreEqual(42, g.EdgeCount);          
        }

        [TestMethod]
        public void ParseTest2ShouldReturnCorrectGraph()
        {
            UndirectedGraph<int, Edge<int>> g;
            List<Tuple<int, int>> pairs;
            string filePath = FindFilePath("test2.txt");
            var result = FileParser.TryParseFile(filePath, out g, out pairs);
            Assert.AreEqual(12, g.VertexCount);
            Assert.AreEqual(86, g.EdgeCount);
        }

        [TestMethod]
        public void ParseTest3ShouldReturnCorrectGraph()
        {
            UndirectedGraph<int, Edge<int>> g;
            List<Tuple<int, int>> pairs;
            string filePath = FindFilePath("test3.txt");
            var result = FileParser.TryParseFile(filePath, out g, out pairs);
            Assert.AreEqual(23, g.VertexCount);
            Assert.AreEqual(394, g.EdgeCount);
        }

        private string FindFilePath(string testFileName)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TestFiles\\" + testFileName;
        }
    }
}
