using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    public class TestModelA: ITestModelA
    {
        public string AA { get; set; }
        /// <summary>
        /// テストモード
        /// </summary>
        public bool IsUnitTestMode { get; set; }

        public TestModelA()
        {

        }

        public TestModelA(string  A)
        {
            AA = A;
        }

        public int Run()
        {
            return 10;
        }
    }

    public interface ITestModelA
    {
        int Run();
    }
}
