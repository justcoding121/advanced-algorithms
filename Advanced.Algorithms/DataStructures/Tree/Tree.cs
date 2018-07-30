using System;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    public class TreeNode<T> : IComparable where T : IComparable
    {
        public T Value { get; set; }

        public TreeNode<T> Parent { get; set; }
        public SinglyLinkedList<TreeNode<T>> Children { get; set; }

        public bool IsLeaf => Children.Count() == 0;

        public TreeNode(TreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;

            Children = new SinglyLinkedList<TreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as TreeNode<T>);
        }

        public int CompareTo(TreeNode<T> treeNode)
        {
            return Value.CompareTo(treeNode.Value);
        }
    }

    public class Tree<T> where T : IComparable
    {
        private TreeNode<T> root { get; set; }
        public int Count { get; private set; }

        //O(n)
        public bool HasItem(T value)
        {
            if (root == null)
            {
                return false;
            }

            return find(root, value) != null;
        }

        //O(n)
        private TreeNode<T> find(T value)
        {
            if (root == null)
            {
                return null;
            }

            return find(root, value);
        }

        //O(n)
        public int GetHeight()
        {
            return getHeight(root);
        }

        //O(n)
        private int getHeight(TreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            var currentHeight = -1;

            foreach (var child in node.Children)
            {
                var childHeight = getHeight(child);

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
        public void Insert(T parentValue, T value)
        {
            if (root == null)
            {
                root = new TreeNode<T>(null, value);
                Count++;
                return;
            }

            var parent = find(parentValue);

            if (parent == null)
            {
                throw new ArgumentNullException();
            }

            var exists = find(root, value) != null;

            if (exists)
            {
                throw new ArgumentException("value already exists");
            }

            parent.Children.InsertFirst(new TreeNode<T>(parent, value));
            Count++;
        }

        public void Delete(T value)
        {
            Delete(root.Value, value);
        }

        //O(n)
        //remove the node with the given identifier from the descendants if it can be deleted unambiguosly
        public void Delete(T parentValue, T value)
        {
            var parent = find(parentValue);

            if (parent == null)
            {
                throw new Exception("Cannot find parent");
            }

            var itemToRemove = find(parent, value);

            if (itemToRemove == null)
            {
                throw new Exception("Cannot find item");
            }

            //if item is root
            if (itemToRemove.Parent == null)
            {
                if (itemToRemove.Children.Count() == 0)
                {
                    root = null;
                }
                else
                {
                    if (itemToRemove.Children.Count() == 1)
                    {
                        root = itemToRemove.Children.DeleteFirst();
                        root.Parent = null;
                    }
                    else
                    {
                        throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                    }
                }

            }
            else
            {
                if (itemToRemove.Children.Count() == 0)
                {
                    itemToRemove.Parent.Children.Delete(itemToRemove);
                }
                else
                {
                    if (itemToRemove.Children.Count() == 1)
                    {
                        var orphan = itemToRemove.Children.DeleteFirst();
                        orphan.Parent = itemToRemove.Parent;

                        itemToRemove.Parent.Children.InsertFirst(orphan);
                        itemToRemove.Parent.Children.Delete(itemToRemove);
                    }
                    else
                    {
                        throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                    }
                }
            }
            Count--;


        }

        //O(n)
        //find the node with the given identifier among descendants of parent
        private TreeNode<T> find(TreeNode<T> parent, T value)
        {
            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            foreach (var child in parent.Children)
            {
                var result = find(child, value);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

    }

}
