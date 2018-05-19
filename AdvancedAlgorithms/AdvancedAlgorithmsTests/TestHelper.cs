using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
