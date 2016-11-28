using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsSkipListNode<T> where T : IComparable
    {
        internal AsSkipListNode<T> Prev { get; set; }
        internal AsSkipListNode<T>[] Next { get; set; }

        internal T value { get; set; }
    }

    public class AsSkipList<T> where T : IComparable
    {
        private Random coinFlipper = new Random();

        public int MaxHeight { get; private set; }

        internal AsSkipListNode<T> Head { get; set; }

        public AsSkipList(int maxHeight = 32)
        {
            MaxHeight = maxHeight;
            Head = new AsSkipListNode<T>()
            {
                Prev = null,
                Next = new AsSkipListNode<T>[maxHeight],
                value = default(T)
            };


        }

        //O(log(n)
        public T Find(T value)
        {
            var current = Head;
            //for each level of linked list from Top
            for (int i = MaxHeight - 1; i >= 0; i--)
            {
                //iterate through the list until match
                //or with an itemless that match
                while (true)
                {
                    if (current.Next[i] != null
                        && current.Next[i].value.CompareTo(value) == 0)
                    {
                        return current.Next[i].value;
                    }

                    if (current.Next[i] == null
                        || current.Next[i].value.CompareTo(value) > 0)
                    {
                        break;
                    }

                    //update current
                    //so that in next level we can start search from here
                    current = current.Next[i];
                }
            }

            return default(T);
        }

        //O(log(n)
        public void Insert(T value)
        {
            //find the random level up to which we link the new node
            var level = 0;
            for (int i = 0; i < MaxHeight
                && coinFlipper.Next(0, 2) == 1; i++)
            {
                level++;
            }

            var newNode = new AsSkipListNode<T>()
            {
                value = value,
                //only level + 1 number of links (level is zero index based)
                Next = new AsSkipListNode<T>[level + 1]
            };

            //init current to head before insertion
            var current = Head;

            //go down from top level
            for (int i = MaxHeight - 1; i >= 0; i--)
            {
                //move on current level of linked list
                //until next element is less than new value to insert
                while (true)
                {
                    if (current.Next[i] == null
                        || current.Next[i].value.CompareTo(value) > 0)
                    {

                        break;
                    }

                    current = current.Next[i];

                }

                //if this level is greater than 
                //maximum levels of link for new node
                //then jump to next level
                if (i > level)
                {
                    continue;
                }

                //insert and update pointers
                newNode.Next[i] = current.Next[i];
                current.Next[i] = newNode;

                //for base level set previous node
                if (i == 0)
                {
                    newNode.Prev = current;
                }
            }
        }

        //O(log(n)
        public void Delete(T value)
        {
            //init current to head before insertion
            var current = Head;

            //go down from top level
            for (int i = MaxHeight - 1; i >= 0; i--)
            {
                //move on current level of linked list
                //until next element is less than new value to delete
                while (true)
                {
                    if (current.Next[i] == null
                        || current.Next[i].value.CompareTo(value) >= 0)
                    {

                        break;
                    }

                    current = current.Next[i];

                }

                if (current.Next[i] != null
                        && current.Next[i].value.CompareTo(value) == 0)
                {

                    //for base level set previous node
                    if (i == 0 && current.Next[i].Next[i] != null)
                    {
                        current.Next[i].Next[i].Prev = current;
                    }

                    //insert and update pointers
                    current.Next[i] = current.Next[i].Next[i];


                }

            }
        }
    }


}
