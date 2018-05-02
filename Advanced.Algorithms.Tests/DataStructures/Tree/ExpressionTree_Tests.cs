using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class ExpressionTree_Tests
    {
        [TestMethod]
        public void ExpressionTree_Smoke_Test()
        {
            var postfixInput = "ab+ef*g*-".ToCharArray();
            var tree = new ExpressionTree<char>();

            tree.Construct(postfixInput, new char[] { '+', '-', '*', '/' });

            var infixResult = tree.GetInfix();
            var expected = "a+b-e*f*g";

            for (int i = 0; i < infixResult.Count; i++)
            {
                Assert.IsTrue(expected[i].Equals(infixResult[i]));
            }
        }
    }
}
