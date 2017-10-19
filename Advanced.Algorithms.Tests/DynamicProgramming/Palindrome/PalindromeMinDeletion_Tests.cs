using Advanced.Algorithms.DynamicProgramming.Palindrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.DynamicProgramming.Palindrome
{
    /// <summary>
    /// Problem statement here
    /// http://www.geeksforgeeks.org/minimum-number-deletions-make-string-palindrome/
    /// </summary>
    [TestClass]
    public class PalindromeMinDeletion_Tests
    {
        [TestMethod]
        public void PalindromeMinDeletion_Smoke_Tests()
        {
            Assert.AreEqual(2, PalindromeMinDeletion.GetMinDeletion("aebcbda"));
            Assert.AreEqual(8, PalindromeMinDeletion.GetMinDeletion("geeksforgeeks"));
        }
    }
}
