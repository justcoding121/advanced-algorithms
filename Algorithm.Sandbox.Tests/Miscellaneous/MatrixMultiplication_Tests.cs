using Algorithm.Sandbox.Miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.Miscellaneous
{
    [TestClass]
    public class MatrixMultication_Tests
    {
        [TestMethod]
        public void MatrixMultication_Smoke_Test()
        {
            var N = 2;
            int[,] A = new int[N, N], B = new int[N, N];

            A[0, 0] = 1; A[0, 1] = 2;
            A[1, 0] = 1;A[1, 1] = 4;

            B[0, 0] = 2; B[0, 1] = 0;
            B[1, 0] = 1; B[1, 1] = 2;

            var result =  MatrixMultiplication.Multiply(A, B);

            Assert.AreEqual(4, result[0, 0]);
            Assert.AreEqual(4, result[0, 1]);
            Assert.AreEqual(6, result[1, 0]);
            Assert.AreEqual(8, result[1, 1]);
        }
    }
}
