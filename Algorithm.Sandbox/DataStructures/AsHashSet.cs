using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsHashSetNode<U, T>
    {
        public U key;
        public T data;

        public AsHashSetNode(U key, T data)
        {
            this.key = key;
            this.data = data;
        }
    }

    /// <summary>
    /// A hash table implementation
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class AsHashSet<U, T>
    {
        private AsSinglyLinkedList<AsHashSetNode<U, T>>[] hashArray;
        private int expectedSize;

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsHashSet(int expectedSize)
        {
            this.expectedSize = expectedSize;
            hashArray = new AsSinglyLinkedList<AsHashSetNode<U, T>>[expectedSize];
        }

        //O(1) time complexity; worst case O(n)
        public bool HasKey(U key)
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
                    if (current.data.key.Equals(key))
                    {
                        return true;
                    }

                    current = current.next;
                }
            }

            return false;
        }

        internal T GetValue(U key)
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
                    if (current.data.key.Equals(key))
                    {
                        return current.data.data;
                    }

                    current = current.next;
                }
            }

            throw new Exception("Item not found");
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(U key, T data)
        {
            var index = Math.Abs(key.GetHashCode()) % expectedSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new AsSinglyLinkedList<AsHashSetNode<U, T>>();
                hashArray[index].AddFirst(new AsHashSetNode<U, T>(key, data));
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.data.key.Equals(key))
                    {
                        throw new Exception("Duplicate key");
                    }

                    current = current.next;
                }

                hashArray[index].AddFirst(new AsHashSetNode<U, T>(key, data));
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
                AsHashSetNode<U, T> item = null;
                while (current != null)
                {
                    if (current.data.key.Equals(key))
                    {
                        item = current.data;
                        break;
                    }

                    current = current.next;
                }

                //remove
                if (item == null)
                {
                    throw new Exception("No such item for given key");
                }
                else
                {

                    hashArray[index].Remove(item);
                }

            }
        }

    }
}
