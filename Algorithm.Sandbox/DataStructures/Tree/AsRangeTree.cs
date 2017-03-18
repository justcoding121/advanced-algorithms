using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// range tree node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsRangeTreeNode<T> : IComparable where T : IComparable
    {
        internal T Data { get; set; }

        internal AsRangeTree<T> tree { get; set; }

        public int CompareTo(object obj)
        {
            return Data.CompareTo(((AsRangeTreeNode<T>)obj).Data);
        }

        public AsRangeTreeNode(T value)
        {
            Data = value;
            tree = new AsRangeTree<T>();
        }
    }

    /// <summary>
    /// range tree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsDRangeTree<T> where T : IComparable
    {
        private int dimensions;
        public int Count { get; private set; }

        private AsRangeTree<T> tree = new AsRangeTree<T>();

        public AsDRangeTree(int dimensions)
        {
            if (dimensions <= 0)
            {
                throw new Exception("Dimension should be greater than 0.");
            }

            this.dimensions = dimensions;
        }

        public void Insert(T[] value)
        {
            validateDimensions(value);

            var currentTree = tree;
            //get all overlaps
            //and insert next dimension value to each overlapping node
            for (int i = 0; i < dimensions; i++)
            {
                currentTree = currentTree.Insert(value[i]).tree;
            }

            Count++;
        }

        public void Delete(T[] value)
        {
            validateDimensions(value);

            DeleteRecursive(tree, value, 0);


            Count--;
        }

        private void DeleteRecursive(AsRangeTree<T> tree, T[] value, int currentDimension)
        {
            if(currentDimension == this.dimensions)
            {
                return;
            }

            var node = tree.Find(value[currentDimension]);
            DeleteRecursive(node.tree, value, currentDimension + 1);
            tree.Delete(value[currentDimension]);
        }

        public AsArrayList<T[]> GetInRange(T[] start, T[] end)
        {
            //validateDimensions(start);
            //validateDimensions(end);

            //var currentTrees = new AsArrayList<AsRangeTree<T>>();

            //currentTrees.Add(tree);

            //var allOverlaps = new AsArrayList<AsRangeTree<T>>();

            //for (int i = 0; i < dimensions; i++)
            //{
            //    allOverlaps = new AsArrayList<AsRangeTree<T>>();

            //    foreach (var tree in currentTrees)
            //    {
            //        var overlaps = tree.GetInRange(start[i], end[i]);

            //        foreach (var overlap in overlaps)
            //        {
            //            allOverlaps.Add(overlap.tree);
            //        }
            //    }

            //    currentTrees = allOverlaps;
            //}
            throw new NotImplementedException();
            
        }

        /// <summary>
        /// validate dimensions for point length
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void validateDimensions(T[] start)
        {
            if (start.Length != dimensions)
            {
                throw new Exception(string.Format("Expecting {0} points.",
                    dimensions));
            }

        }
    }

    /// <summary>
    /// One dimensional range tree
    /// TODO support multiple dimensions 
    /// by nesting node with r-b tree for next dimension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsRangeTree<T> where T : IComparable
    {
        internal AsRedBlackTree<AsRangeTreeNode<T>> tree
            = new AsRedBlackTree<AsRangeTreeNode<T>>();

        internal int Count => tree.Count;

        public AsRangeTreeNode<T> Find(T value)
        {
            return tree.Find(new AsRangeTreeNode<T>(value)).Value;
        }

        internal AsRangeTreeNode<T> Insert(T value)
        {
            var newNode = new AsRangeTreeNode<T>(value);
            var result = tree.InsertAndReturnNewNode(newNode);
            return result.Value;
        }

        internal void Delete(T value)
        {
            tree.Delete(new AsRangeTreeNode<T>(value));
        }

        internal AsArrayList<AsRangeTreeNode<T>> GetInRange(T start, T end)
        {
            return GetInRange(new AsArrayList<AsRangeTreeNode<T>>(),
                new AsDictionary<AsRedBlackTreeNode<AsRangeTreeNode<T>>, bool>(),
                tree.Root, start, end);
        }

        private AsArrayList<AsRangeTreeNode<T>> GetInRange(AsArrayList<AsRangeTreeNode<T>> result,
            AsDictionary<AsRedBlackTreeNode<AsRangeTreeNode<T>>, bool> visited,
            AsRedBlackTreeNode<AsRangeTreeNode<T>> currentNode,
            T start, T end)
        {

            if (currentNode.IsLeaf)
            {
                //start is less than current node
                if (InRange(currentNode, start, end))
                {
                    foreach (var v in currentNode.Values)
                    {
                        result.Add(v);
                    }
                }
            }
            //if start is less than current
            //move left
            else
            {
                if (start.CompareTo(currentNode.Value.Data) <= 0)
                {
                    if (currentNode.Left != null)
                    {
                        GetInRange(result, visited, currentNode.Left, start, end);
                    }


                    //start is less than current node
                    if (!visited.ContainsKey(currentNode)
                        && InRange(currentNode, start, end))
                    {
                        foreach (var v in currentNode.Values)
                        {
                            result.Add(v);
                        }

                        visited.Add(currentNode, false);
                    }
                }
                //if start is greater than current
                //and end is greater than current
                //move right
                if (end.CompareTo(currentNode.Value.Data) >= 0)
                {
                    if (currentNode.Right != null)
                    {
                        GetInRange(result, visited, currentNode.Right, start, end);
                    }

                    //start is less than current node
                    if (!visited.ContainsKey(currentNode)
                        && InRange(currentNode, start, end))
                    {
                        foreach (var v in currentNode.Values)
                        {
                            result.Add(v);
                        }

                        visited.Add(currentNode, false);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// checks if current node is in search range
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private bool InRange(AsRedBlackTreeNode<AsRangeTreeNode<T>> currentNode, T start, T end)
        {
            //start is less than current & end is greater than current
            return start.CompareTo(currentNode.Value.Data) <= 0
                && end.CompareTo(currentNode.Value.Data) >= 0;
        }
    }
}
