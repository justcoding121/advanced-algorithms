using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeNode<T> : IComparable where T : IComparable
    {
        public T Value { get; set; }

        public AsTreeNode<T> Parent { get; private set; }
        public AsSinglyLinkedList<AsTreeNode<T>> Children { get; private set; }

        public bool IsLeaf => Children.Count() == 0;

        public AsTreeNode(AsTreeNode<T> parent, T value)
        {
            this.Parent = parent;
            this.Value = value;

            Children = new AsSinglyLinkedList<AsTreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as AsTreeNode<T>);
        }

        public int CompareTo(AsTreeNode<T> treeNode)
        {
            return Value.CompareTo(treeNode.Value);
        }

    }

    public class AsTree<T> where T : IComparable
    {
        public AsTreeNode<T> Root { get; set; }

        //constructor
        public AsTree(T value)
        {
            Root = new AsTreeNode<T>(null, value);
        }

        //O(n)
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Root.Find(value) != null;
        }

        //O(n)
        public AsTreeNode<T> Find(T value)
        {
            if (Root == null)
            {
                return null;
            }

            return Root.Find(value);
        }

        public int GetHeight()
        {
            return Root.GetHeight();
        }

    }
    public static class AsTreeExtensions
    {
        public static int GetHeight<T>(this AsTreeNode<T> node) where T : IComparable
        {
            if (node == null)
            {
                return -1;
            }

            var children = node.Children.GetAllNodes();

            int currentHeight = -1;

            for (int i = 0; i < children.Length; i++)
            {
                var childHeight = GetHeight(children.ItemAt(i));

                if (currentHeight < childHeight)
                {
                    currentHeight = childHeight;
                }
            }

            currentHeight++;

            return currentHeight;
        }

        //add the new child under this parent
        public static void AddAsDirectChild<T>(this AsTreeNode<T> parent, AsTreeNode<T> treeRoot, T value) where T : IComparable
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            var exists = Find(treeRoot, value) != null;

            if (exists)
            {
                throw new ArgumentNullException("value already exists");
            }

            parent.Children.InsertFirst(new AsTreeNode<T>(parent, value));
        }

        //remove the node with the given identifier from the descendants 
        public static void RemoveFromDescendents<T>(this AsTreeNode<T> parent, AsTreeNode<T> treeRoot, T value) where T : IComparable
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            var itemToRemove = Find(parent, value);

            if (itemToRemove != null)
            {
                //if item is root
                if (itemToRemove.Parent == null)
                {
                    treeRoot = null;
                }
                else
                {
                    //add childrens to its grand parent
                    var childrens = itemToRemove.Children.GetAllNodes();

                    for (int i = 0; i < childrens.Length; i++)
                    {
                        itemToRemove.Parent.Children.InsertFirst(childrens.ItemAt(i));
                    }

                    itemToRemove.Parent.Children.Delete(itemToRemove);
                    itemToRemove = null;
                }

                return;
            }

            throw new Exception("Item not found");
        }

        //find the node with the given identifier among descendants of parent
        public static AsTreeNode<T> Find<T>(this AsTreeNode<T> parent, T value) where T : IComparable
        {

            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            var children = parent.Children.GetAllNodes();

            for (int i = 0; i < children.Length; i++)
            {
                var result = Find(children.ItemAt(i), value);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
