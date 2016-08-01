using System;
using System.Collections;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeNode<I, V> : IComparer
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsSinglyLinkedList<AsTreeNode<I, V>> Children { get; set; }

        public bool IsLeaf => Children.Count() == 0;

        public AsTreeNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;

            Children = new AsSinglyLinkedList<AsTreeNode<I, V>>();
        }

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }
    }

    public class AsTree<I, V>
    {
        public AsTreeNode<I, V> Root { get; set; }

        public AsTree(I identifier, V value)
        {
            Root = new AsTreeNode<I, V>(identifier, value);
        }
        //O(1)
        public void AddToRoot(I identifier, V value)
        {
            if (Root == null)
            {
                Root = new AsTreeNode<I, V>(identifier, value);
                return;
            }
            else
            {
                Root.Children.InsertFirst(new AsTreeNode<I, V>(identifier, value));
            }

        }
        //O(n)
        public bool HasItem(I identifier)
        {
            if (Root == null)
            {
                return false;
            }

            return Root.Find(identifier) != null;
        }

        //O(n)
        public AsTreeNode<I, V> Find(I identifier)
        {
            if (Root == null)
            {
                return null;
            }

            return Root.Find(identifier);
        }
    }
    public static class AsTreeExtensions
    {
        //O(1)
        public static void Add<I, V>(this AsTreeNode<I, V> parent, I identifier, V value)
        {
            parent.Children.InsertFirst(new AsTreeNode<I, V>(identifier, value));
        }

        public static void Remove<I, V>(this AsTreeNode<I, V> parent, I identifier)
        {
            var parentNode = FindParent(parent, identifier);

            RemoveFromImmediateChildren(parentNode, identifier);
           
        }

        public static void RemoveFromImmediateChildren<I,V>(this AsTreeNode<I, V> parent, I identifier)
        {
            var children = parent.Children.GetAllNodes();

            for (int i = 0; i < children.Length; i++)
            {
                if (children.ItemAt(i).Identifier.Equals(identifier))
                {
                    parent.Children.Delete(children.ItemAt(i));
                }
            }
        }


        //O(n)
        public static AsTreeNode<I, V> Find<I,V>(this AsTreeNode<I, V> node, I identifier)
        {

            if (node.Identifier.Equals(identifier))
            {
                return node;
            }

            var children = node.Children.GetAllNodes();

            for (int i = 0; i < children.Length; i++)
            {
                var result = Find(children.ItemAt(i), identifier);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        //O(n)
        public static AsTreeNode<I, V> FindParent<I, V>(this AsTreeNode<I, V> node, I identifier)
        {

            var children = node.Children.GetAllNodes();

            for (int i = 0; i < children.Length; i++)
            {
                if(children.ItemAt(i).Identifier.Equals(identifier))
                {
                    return node;
                }

                var result = children.ItemAt(i).FindParent(identifier);

                if(result!=null)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
