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
        public void SmokeTest_IsEven()
        {
            Assert.IsTrue(BitHacks.IsEven(22));
            Assert.IsFalse(BitHacks.IsEven(101));
        }

        /// <summary>
        /// Checks if given number is a power of 2
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_IsPowerOf2()
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
        public void SmokeTest_HasOppositeSigns()
        {
            Assert.IsTrue(BitHacks.HasOppositeSigns(-11, 22));
            Assert.IsFalse(BitHacks.HasOppositeSigns(21, 22));
        }

        /// <summary>
        /// Checks if nth bit from right is set, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public void SmokeTest_IsSet()
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
        public void SmokeTest_SetBit()
        {
            var binaryString = "1100";
            Assert.AreEqual(Convert.ToString(BitHacks.SetBit(Convert.ToInt32(binaryString, 2), 2), 2).Substring(32 - 4, 4), "1110");
        }

        /// <summary>
        /// Unsets nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public void SmokeTest_UnsetBit()
        {
            var binaryString = "1100";
            Assert.AreEqual(Convert.ToString(BitHacks.UnsetBit(Convert.ToInt32(binaryString, 2), 3), 2).Substring(32 - 4, 4), "0100");
        }

        /// <summary>
        /// Toggles nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public void SmokeTest_ToggleBit()
        {
            var binaryString = "1110";
            Assert.AreEqual(Convert.ToString(BitHacks.ToggleBit(Convert.ToInt32(binaryString, 2), 1), 2).Substring(32 - 4, 4), "1110");
        }

        /// <summary>
        ///  Turns On first Unset bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_TurnOnRightmostUnsetBit()
        {
            var binaryString = "1100";
            Assert.AreEqual(Convert.ToString(BitHacks.TurnOnRightmostUnsetBit(Convert.ToInt32(binaryString, 2)), 2).Substring(32 - 4, 4), "1110");
        }

        /// <summary>
        /// Turns Off first set bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_TurnOffRightmostSetBit()
        {
            var binaryString = "1100";
            Assert.AreEqual(Convert.ToString(BitHacks.TurnOffRightmostSetBit(Convert.ToInt32(binaryString, 2)), 2).Substring(32 - 4, 4), "1000");
        }

        /// <summary>
        /// Gets the first right most sub bits starting with a set bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_GetRightmostSubBitsStartingWithASetBit()
        {
            var binaryString = "1100";
            Assert.AreEqual(Convert.ToString(BitHacks.GetRightmostSubBitsStartingWithASetBit(Convert.ToInt32(binaryString, 2)), 2).Substring(32 - 4, 4), "0100");
        }

        /// <summary>
        ///  Gets the first right most sub bits starting with a Unset bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_GetRightmostSubBitsStartingWithAnUnsetBit()
        {
            var binaryString = "1011";
            Assert.AreEqual(Convert.ToString(BitHacks.GetRightmostSubBitsStartingWithASetBit(Convert.ToInt32(binaryString, 2)), 2).Substring(32 - 4, 4), "0011");
        }

        /// <summary>
        ///  Sets all the first right most sub bits starting with a set bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_RightPropogateRightmostSetBit()
        {
            var binaryString = "1100";
            Assert.AreEqual(Convert.ToString(BitHacks.RightPropogateRightmostSetBit(Convert.ToInt32(binaryString, 2)), 2).Substring(32 - 4, 4), "1111");
        }

        /// <summary>
        ///  Sets all the first right most sub bits starting with a unset bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SmokeTest_RightPropogateRightmostUnsetBit()
        {
            var binaryString = "1011";
            Assert.AreEqual(Convert.ToString(BitHacks.RightPropogateRightmostUnsetBit(Convert.ToInt32(binaryString, 2)), 2).Substring(32 - 4, 4), "1000");
        }

        /// <summary> 
        /// Update the nth bit from right with given voidean value, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SmokeTest_UpdateBitToValue()
        {
            var binaryString = "1011";
            Assert.AreEqual(Convert.ToString(BitHacks.UpdateBitToValue(Convert.ToInt32(binaryString, 2), 3, true), 2).Substring(32 - 4, 4), "1111");
        }
    }
}
