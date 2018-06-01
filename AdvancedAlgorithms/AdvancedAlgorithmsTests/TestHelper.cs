using System.IO;
using System.Reflection;

namespace AdvancedAlgorithmsTests
{
    public static class TestHelper
    {
        public static string FindFilePath(string testFileName)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TestFiles\\" + testFileName;
        }
    }
}
