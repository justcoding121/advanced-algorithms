using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A hash table implementation (key value dictionary)
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsHashSet<K, V> where K : IComparable
    {
        /// <summary>
        /// key-value set
        /// </summary>
        private class AsHashSetNode : IComparable
        {
            public K Key;
            public V Value;

            public AsHashSetNode(K key, V value)
            {
                this.Key = key;
                this.Value = value;
            }

            public int CompareTo(object obj)
            {
                return CompareTo(obj as AsHashSetNode);
            }

            private int CompareTo(AsHashSetNode node)
            {
                return Key.CompareTo(node.Key);
            }
        }

        private AsSinglyLinkedList<AsHashSetNode>[] hashArray;
        private int expectedSize;

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsHashSet(int expectedSize)
        {
            this.expectedSize = expectedSize;
            hashArray = new AsSinglyLinkedList<AsHashSetNode>[expectedSize];
        }

        //O(1) time complexity; worst case O(n)
        public bool ContainsKey(K key)
        {
            var index = Math.Abs(key.GetHashCode()) % expectedSize;

            if (hashArray[index] == null)
            {
                return false;
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key)==0)
                    {
                        return true;
                    }

                    current = current.Next;
                }
            }

            return false;
        }

        public V GetValue(K key)
        {
            var index = Math.Abs(key.GetHashCode()) % expectedSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key)==0)
                    {
                        return current.Data.Value;
                    }

                    current = current.Next;
                }
            }

            throw new Exception("Item not found");
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(K key, V value)
        {
            var index = Math.Abs(key.GetHashCode()) % expectedSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new AsSinglyLinkedList<AsHashSetNode>();
                hashArray[index].InsertFirst(new AsHashSetNode(key, value));
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key)==0)
                    {
                        throw new Exception("Duplicate key");
                    }

                    current = current.Next;
                }

                hashArray[index].InsertFirst(new AsHashSetNode(key, value));
            }
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(K key)
        {
            var index = Math.Abs(key.GetHashCode()) % expectedSize;

            if (hashArray[index] == null)
            {
                throw new Exception("No such item for given key");
            }
            else
            {
                var current = hashArray[index].Head;

                //VODO merge both search and remove to a single loop here!
                AsHashSetNode item = null;
                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key)==0)
                    {
                        item = current.Data;
                        break;
                    }

                    current = current.Next;
                }

                //remove
                if (item == null)
                {
                    throw new Exception("No such item for given key");
                }
                else
                {

                    hashArray[index].Delete(item);
                }

            }
        }

    }
}
