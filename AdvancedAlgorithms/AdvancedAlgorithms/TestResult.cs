using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgorithms
{
    public struct TestResult
    {
        public TestResult(bool isCorrect, string errorMessage)
        {
            this.IsCorrect = isCorrect;
            this.ErrorMessage = errorMessage;
        }
        public bool IsCorrect;
        public string ErrorMessage;
    }
}
