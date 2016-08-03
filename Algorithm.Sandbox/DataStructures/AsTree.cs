using System;

namespace Algorithm.Sandbox.DataStructures
{

    public class AsTree<T> where T : IComparable
    {
        private class AsTreeNode<T> : IComparable where T : IComparable
        {
            public T Value { get; set; }

            public AsTreeNode<T> Parent { get; set; }
            public AsSinglyLinkedList<AsTreeNode<T>> Children { get; set; }

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

  

        private AsTreeNode<T> Root { get; set; }
        public int Count { get; private set; }

        //constructor
        public AsTree(T value)
        {
            Root = new AsTreeNode<T>(null, value);
            Count++;
        }

        //O(n)
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }

        //O(n)
        private AsTreeNode<T> Find(T value)
        {
            if (Root == null)
            {
                return null;
            }

            return Find(Root, value);
        }

        //O(n)
        public int GetHeight()
        {
            return GetHeight(Root);
        }
        //O(n)
        private int GetHeight(AsTreeNode<T> node)
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

        //O(1)
        //add the new child under this parent
        public void Add(T parentValue, T value) 
        {
            var parent = Find(parentValue);

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
           
            var exists = Find(Root, value) != null;

            if (exists)
            {
                throw new ArgumentNullException("value already exists");
            }

            parent.Children.InsertFirst(new AsTreeNode<T>(parent, value));
            Count++;
        }

        public void Remove(T value)
        {
            Remove(Root.Value, value);
        }

        //O(n)
        //remove the node with the given identifier from the descendants 
        public void Remove(T parentValue, T value)
        {
            var parent = Find(parentValue);

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
                    if(itemToRemove.Children.Count() == 0)
                    {
                        Root = null;
                    }
                    else
                    {
                        //pick a child as root
                        Root = itemToRemove.Children.DeleteFirst();

                        //now add remaining children to root
                        var childrens = itemToRemove.Children.GetAllNodes();

                        for (int i = 0; i < childrens.Length; i++)
                        {
                            Root.Children.InsertFirst(childrens.ItemAt(i));
                        }

                        Root.Parent = null;
                    }
                  
                }
                else
                {
                    //add childrens to its grand parent
                    var childrens = itemToRemove.Children.GetAllNodes();

                    for (int i = 0; i < childrens.Length; i++)
                    {
                        childrens.ItemAt(i).Parent = itemToRemove.Parent;
                        itemToRemove.Parent.Children.InsertFirst(childrens.ItemAt(i));
                    }

                    itemToRemove.Parent.Children.Delete(itemToRemove);
                    itemToRemove = null;
                }
                Count--;
                return;
            }

            throw new Exception("Item not found");
        }

        //O(n)
        //find the node with the given identifier among descendants of parent
        private AsTreeNode<T> Find(AsTreeNode<T> parent, T value) 
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
