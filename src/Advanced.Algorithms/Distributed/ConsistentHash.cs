using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Advanced.Algorithms.Distributed
{
    /// <summary>
    /// A consistant hash implementation with murmur hash.
    /// </summary>
    public class ConsistentHash<T>
    {
        readonly SortedDictionary<int, T> circle = new SortedDictionary<int, T>();
        private int[] circleKeys;
        readonly int replicas;

        public ConsistentHash()
            : this(new List<T>(), 100) { }

        public ConsistentHash(IEnumerable<T> nodes, int replicas)
        {
            this.replicas = replicas;
            foreach (var node in nodes)
            {
                AddNode(node);
            }
        }

        /// <summary>
        /// Add a new bucket.
        /// </summary>
        public void AddNode(T node)
        {
            for (var i = 0; i < replicas; i++)
            {
                var hash = getHashCode(node.GetHashCode().ToString() + i);
                circle[hash] = node;
            }

            circleKeys = circle.Keys.ToArray();
        }

        /// <summary>
        /// Get the bucket for the given Key.
        /// </summary>
        public T GetNode(string key)
        {
            var hash = getHashCode(key);
            var first = nextClockWise(circleKeys, hash);
            return circle[circleKeys[first]];
        }

        /// <summary>
        /// Remove a bucket from lookup.
        /// </summary>
        public void RemoveNode(T node)
        {
            for (var i = 0; i < replicas; i++)
            {
                var hash = getHashCode(node.GetHashCode().ToString() + i);
                if (!circle.Remove(hash))
                {
                    throw new Exception("Cannot remove a node that was never added.");
                }
            }

            circleKeys = circle.Keys.ToArray();
        }


        /// <summary>
        /// Move clockwise until we find a bucket with Key >= hashCode
        /// </summary>
        /// <returns>Returns the index of bucket</returns>
        private int nextClockWise(int[] keys, int hashCode)
        {
            var begin = 0;
            var end = keys.Length - 1;

            if (keys[end] < hashCode || keys[0] > hashCode)
            {
                return 0;
            }

            //do a binary search
            while (end - begin > 1)
            {
                var mid = (end + begin) / 2;
                if (keys[mid] >= hashCode)
                {
                    end = mid;
                }
                else
                {
                    begin = mid;
                }
            }

            return end;
        }

        private static int getHashCode(string key)
        {
            return (int)MurmurHash2.Hash(Encoding.Unicode.GetBytes(key));
        }

    }

    /// <summary>
    /// Adapted from https://github.com/wsq003/consistent-hash/blob/master/ConsistentHash.cs
    /// </summary>
    internal class MurmurHash2
    {
        internal static UInt32 Hash(Byte[] data)
        {
            return Hash(data, 0xc58f1a7b);
        }
        const UInt32 m = 0x5bd1e995;
        const Int32 r = 24;

        [StructLayout(LayoutKind.Explicit)]
        struct BytetoUInt32Converter
        {
            [FieldOffset(0)]
            public Byte[] Bytes;

            [FieldOffset(0)]
            public UInt32[] UInts;
        }

        internal static UInt32 Hash(Byte[] data, UInt32 seed)
        {
            Int32 length = data.Length;
            if (length == 0)
                return 0;
            UInt32 h = seed ^ (UInt32)length;
            Int32 currentIndex = 0;
            // array will be length of Bytes but contains Uints
            // therefore the currentIndex will jump with +1 while length will jump with +4
            UInt32[] hackArray = new BytetoUInt32Converter { Bytes = data }.UInts;
            while (length >= 4)
            {
                UInt32 k = hackArray[currentIndex++];
                k *= m;
                k ^= k >> r;
                k *= m;

                h *= m;
                h ^= k;
                length -= 4;
            }
            currentIndex *= 4; // fix the length
            switch (length)
            {
                case 3:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)data[currentIndex] << 16;
                    h *= m;
                    break;
                case 2:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= m;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= m;
                    break;
                default:
                    break;
            }

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return h;
        }
    }
}

