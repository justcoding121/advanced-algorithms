using Algorithm.Sandbox.BitAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    public class DrawLine_Tests
    {
        public void DrawLine_Smoke_Test()
        {
            DrawLine.Draw(new byte[100], 10, 5, 9, 50);
        }
    }
}
