using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    [TestClass]
    public class BitHacks_Tests
    {
        /// <summary>
        /// Checks if given number is even
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void IsEven_Smoke_Test()
        {
            Assert.IsTrue(BitHacks.IsEven(22));
            Assert.IsFalse(BitHacks.IsEven(101));
        }

        /// <summary>
        /// Checks if given number is a power of 2
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void IsPowerOf2_Smoke_Test()
        {
            Assert.IsTrue(BitHacks.IsPowerOf2(32));
            Assert.IsFalse(BitHacks.IsPowerOf2(22));
        }

        /// <summary>
        /// Checks if given numbers are of opposite signs
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [TestMethod]
        public void HasOppositeSigns_Smoke_Test()
        {
            Assert.IsTrue(BitHacks.HasOppositeSigns(11, -22));
            Assert.IsTrue(BitHacks.HasOppositeSigns(-11, 22));

            Assert.IsFalse(BitHacks.HasOppositeSigns(21, 22));
            Assert.IsFalse(BitHacks.HasOppositeSigns(-21, -22));
        }

        /// <summary>
        /// Checks if nth bit from right is set, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        [TestMethod]
        public void IsSet_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.IsTrue(BitHacks.IsSet(Convert.ToInt32(binaryString, 2), 2));
            Assert.IsFalse(BitHacks.IsSet(Convert.ToInt32(binaryString, 2), 1));
        }

        /// <summary>
        /// Sets nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        [TestMethod]
        public void SetBit_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.AreEqual("1110", Convert.ToString(BitHacks.SetBit(Convert.ToInt32(binaryString, 2), 1), 2));
        }

        /// <summary>
        /// Unsets nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        [TestMethod]
        public void UnsetBit_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.AreEqual("100", Convert.ToString(BitHacks.UnsetBit(Convert.ToInt32(binaryString, 2), 3), 2));
        }

        /// <summary>
        /// Toggles nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        [TestMethod]
        public void ToggleBit_Smoke_Test()
        {
            var binaryString = "1110";
            Assert.AreEqual(Convert.ToString(BitHacks.ToggleBit(Convert.ToInt32(binaryString, 2), 1), 2), "1100");
        }

        /// <summary>
        ///  Turns On first Unset bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void TurnOnBitAfterRightmostSetBit_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.AreEqual("1110", Convert.ToString(BitHacks.TurnOnBitAfterRightmostSetBit(Convert.ToInt32(binaryString, 2)), 2));

            binaryString = "1101";
            Assert.AreEqual("1101", Convert.ToString(BitHacks.TurnOnBitAfterRightmostSetBit(Convert.ToInt32(binaryString, 2)), 2));

        }

        /// <summary>
        /// Turns Off first set bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void TurnOffRightmostSetBit_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.AreEqual("1000", Convert.ToString(BitHacks.TurnOffRightmostSetBit(Convert.ToInt32(binaryString, 2)), 2));
        }

        /// <summary>
        /// Gets the first right most sub bits starting with a set bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void GetRightmostSubBitsStartingWithASetBit_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.AreEqual("100",Convert.ToString(BitHacks.GetRightmostSubBitsStartingWithASetBit(Convert.ToInt32(binaryString, 2)), 2));
        }

        /// <summary>
        ///  Gets the first right most sub bits starting with a Unset bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void GetRightmostSubBitsStartingWithAnUnsetBit_Smoke_Test()
        {
            var binaryString = "1011";
            Assert.AreEqual("11",Convert.ToString(BitHacks.GetRightmostSubBitsStartingWithAnUnsetBit(Convert.ToInt32(binaryString, 2)), 2));
        }

        /// <summary>
        ///  Sets all the first right most sub bits starting with a set bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void RightPropogateRightmostSetBit_Smoke_Test()
        {
            var binaryString = "1100";
            Assert.AreEqual("1111", Convert.ToString(BitHacks.RightPropogateRightmostSetBit(Convert.ToInt32(binaryString, 2)), 2));
        }

        /// <summary>
        ///  Sets all the first right most sub bits starting with a unset bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [TestMethod]
        public void RightPropogateRightmostUnsetBit_Smoke_Test()
        {
            var binaryString = "1011";
            Assert.AreEqual("1000", Convert.ToString(BitHacks.RightPropogateRightmostUnsetBit(Convert.ToInt32(binaryString, 2)), 2));
        }

        /// <summary> 
        /// Update the nth bit from right with given voidean value, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [TestMethod]
        public void UpdateBitToValue_Smoke_Test()
        {
            var binaryString = "1011";
            Assert.AreEqual("1111", Convert.ToString(BitHacks.UpdateBitToValue(Convert.ToInt32(binaryString, 2), 2, true), 2));

            binaryString = "1111";
            Assert.AreEqual("1011", Convert.ToString(BitHacks.UpdateBitToValue(Convert.ToInt32(binaryString, 2), 2, false), 2));
        }


        /// <summary> 
        /// Count set bits in given integer
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [TestMethod]
        public void CountSetBits_Smoke_Test()
        {
            var binaryString = "1011";
            Assert.AreEqual(3, BitHacks.CountSetBits(Convert.ToInt32(binaryString, 2)));

            binaryString = "1111";
            Assert.AreEqual(4, BitHacks.CountSetBits(Convert.ToInt32(binaryString, 2)));
        }

        /// <summary> 
        /// Count trailing zero bits efficients using Binary Search
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [TestMethod]
        public void CountTrailingZerosUsingBinarySearch_Smoke_Test()
        {
            var binaryString = "1000";
            Assert.AreEqual(3, BitHacks.CountTrailingZerosByBinarySearch(Convert.ToInt32(binaryString, 2)));

            binaryString = "1111";
            Assert.AreEqual(0, BitHacks.CountTrailingZerosByBinarySearch(Convert.ToInt32(binaryString, 2)));

            binaryString = "11110110000000000000000000000000";
            Assert.AreEqual(25, BitHacks.CountTrailingZerosByBinarySearch(Convert.ToInt32(binaryString, 2)));
        }
    }
}
