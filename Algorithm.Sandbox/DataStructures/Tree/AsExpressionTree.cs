using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    /// <summary>
    /// expression tree node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsExpressionTreeNode<T>
    {
        internal T Value { get; set; }
        internal AsExpressionTreeNode<T> Left { get; set; }
        internal AsExpressionTreeNode<T> Right { get; set; }

        internal AsExpressionTreeNode(T value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// A Expression tree implementation (POSIX tree)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsExpressionTree<T>
    {
        /// <summary>
        /// expression stack
        /// </summary>
        private Stack<AsExpressionTreeNode<T>> expStack;

        /// <summary>
        /// construct tree for given expression with given operators
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="operators"></param>
        public void Construct(T[] expression, T[] operators)
        {
            expStack = new Stack<AsExpressionTreeNode<T>>();

            for (int i = 0; i < expression.Length; i++)
            {
                var newNode = new AsExpressionTreeNode<T>(expression[i]);

                if (Contains(operators, expression[i]))
                {
                    var right = expStack.Pop();
                    var left = expStack.Pop();

                    newNode.Left = left;
                    newNode.Right = right;

                }

                expStack.Push(newNode);
            }
        }

        /// <summary>
        /// check if array contains the test item
        /// </summary>
        /// <param name="operators"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        private bool Contains(T[] operators, T test)
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (operators[i].Equals(test))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// get infix expression
        /// </summary>
        /// <returns></returns>
        public List<T> GetInfix()
        {
            if (expStack == null || expStack.Count == 0)
            {
                throw new Exception("Tree is empty");
            }

            if (expStack.Count != 1)
            {
                throw new Exception("Erroneous input expression.");
            }

            var root = expStack.Pop();

            return VisitInOrder(new List<T>(), root);
        }

        /// <summary>
        /// make an inorder traversal and compile the infix expression
        /// </summary>
        /// <param name="result"></param>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private List<T> VisitInOrder(List<T> result, AsExpressionTreeNode<T> currentNode)
        {
            if (currentNode == null)
            {
                return result;
            }

            VisitInOrder(result, currentNode.Left);
            result.Add(currentNode.Value);
            VisitInOrder(result, currentNode.Right);

            return result;
        }
    }
}
