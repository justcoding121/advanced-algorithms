using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     A consistant hash implementation with murmur hash.
/// </summary>
public class ConsistentHash<T>
{
    private readonly SortedDictionary<int, T> circle = new();
    private readonly int replicas;
    private int[] circleKeys;

    public ConsistentHash()
        : this(new List<T>(), 100)
    {
    }

    public ConsistentHash(IEnumerable<T> nodes, int replicas)
    {
        this.replicas = replicas;
        foreach (var node in nodes) AddNode(node);
    }

    /// <summary>
    ///     Add a new bucket.
    /// </summary>
    public void AddNode(T node)
    {
        for (var i = 0; i < replicas; i++)
        {
            var hash = GetHashCode(node.GetHashCode().ToString() + i);
            circle[hash] = node;
        }

        circleKeys = circle.Keys.ToArray();
    }

    /// <summary>
    ///     Get the bucket for the given Key.
    /// </summary>
    public T GetNode(string key)
    {
        var hash = GetHashCode(key);
        var first = NextClockWise(circleKeys, hash);
        return circle[circleKeys[first]];
    }

    /// <summary>
    ///     Remove a bucket from lookup.
    /// </summary>
    public void RemoveNode(T node)
    {
        for (var i = 0; i < replicas; i++)
        {
            var hash = GetHashCode(node.GetHashCode().ToString() + i);
            if (!circle.Remove(hash)) throw new Exception("Cannot remove a node that was never added.");
        }

        circleKeys = circle.Keys.ToArray();
    }


    /// <summary>
    ///     Move clockwise until we find a bucket with Key >= hashCode
    /// </summary>
    /// <returns>Returns the index of bucket</returns>
    private int NextClockWise(int[] keys, int hashCode)
    {
        var begin = 0;
        var end = keys.Length - 1;

        if (keys[end] < hashCode || keys[0] > hashCode) return 0;

        //do a binary search
        while (end - begin > 1)
        {
            var mid = (end + begin) / 2;
            if (keys[mid] >= hashCode)
                end = mid;
            else
                begin = mid;
        }

        return end;
    }

    private static int GetHashCode(string key)
    {
        return (int)MurmurHash2.Hash(Encoding.Unicode.GetBytes(key));
    }
}

/// <summary>
///     Adapted from https://github.com/wsq003/consistent-hash/blob/master/ConsistentHash.cs
/// </summary>
internal class MurmurHash2
{
    private const uint M = 0x5bd1e995;
    private const int R = 24;

    internal static uint Hash(byte[] data)
    {
        return Hash(data, 0xc58f1a7b);
    }

    internal static uint Hash(byte[] data, uint seed)
    {
        var length = data.Length;
        if (length == 0)
            return 0;
        var h = seed ^ (uint)length;
        var currentIndex = 0;
        // array will be length of Bytes but contains Uints
        // therefore the currentIndex will jump with +1 while length will jump with +4
        var hackArray = new BytetoUInt32Converter { Bytes = data }.UInts;
        while (length >= 4)
        {
            var k = hackArray[currentIndex++];
            k *= M;
            k ^= k >> R;
            k *= M;

            h *= M;
            h ^= k;
            length -= 4;
        }

        currentIndex *= 4; // fix the length
        switch (length)
        {
            case 3:
                h ^= (ushort)(data[currentIndex++] | (data[currentIndex++] << 8));
                h ^= (uint)data[currentIndex] << 16;
                h *= M;
                break;
            case 2:
                h ^= (ushort)(data[currentIndex++] | (data[currentIndex] << 8));
                h *= M;
                break;
            case 1:
                h ^= data[currentIndex];
                h *= M;
                break;
        }

        // Do a few final mixes of the hash to ensure the last few
        // bytes are well-incorporated.

        h ^= h >> 13;
        h *= M;
        h ^= h >> 15;

        return h;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct BytetoUInt32Converter
    {
        [FieldOffset(0)] public byte[] Bytes;

        [FieldOffset(0)] public readonly uint[] UInts;
    }
}