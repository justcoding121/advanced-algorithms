using System;

namespace Algorithm.Sandbox.DataStructures
{

    /// <summary>
    /// A hash table implementation (key value dictionary)
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class AsHashSet<U, T>
    {
        /// <summary>
        /// key-value set
        /// </summary>
        private class AsHashSetNode
        {
            public U Key;
            public T Value;

            public AsHashSetNode(U key, T value)
            {
                this.Key = key;
                this.Value = value;
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
        public bool ContainsKey(U key)
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
                    if (current.Data.Key.Equals(key))
                    {
                        return true;
                    }

                    current = current.Next;
                }
            }

            return false;
        }

        public T GetValue(U key)
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
                    if (current.Data.Key.Equals(key))
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
        public void Add(U key, T value)
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
                    if (current.Data.Key.Equals(key))
                    {
                        throw new Exception("Duplicate key");
                    }

                    current = current.Next;
                }

                hashArray[index].InsertFirst(new AsHashSetNode(key, value));
            }
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(U key)
        {
            var index = Math.Abs(key.GetHashCode()) % expectedSize;

            if (hashArray[index] == null)
            {
                throw new Exception("No such item for given key");
            }
            else
            {
                var current = hashArray[index].Head;

                //TODO merge both search and remove to a single loop here!
                AsHashSetNode item = null;
                while (current != null)
                {
                    if (current.Data.Key.Equals(key))
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
