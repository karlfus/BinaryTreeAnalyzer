using BinaryTreeAnalyzer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeAnalyzer
{
    public static class Analyzer
    {
        /// <summary>
        /// Find the common parent of two nodes in a binary tree
        /// </summary>
        public static Node ParentFinder(Node tree, Node n1, Node n2)
        {
            var path1 = FindPath(tree, n1).ToList();
            var path2 = FindPath(tree, n2).ToList();

            Node lastNode = null;
            var j = path2.Count;

            //walk down each path until we find the divergent node
            for (var i = path1.Count - 1; i >= 0; i--)
            {
                j--;
                if (j >= 0)
                {
                    if (path1[i] != path2[j])
                    {
                        return lastNode;
                    }
                }
                lastNode = path1[i];
            }
            
            //no result found
            return null;
        }

        /// <summary>
        /// Find a node in a tree and return its path as a top-down IEnumerable of Nodes
        /// </summary>
        private static IEnumerable<Node> FindPath(Node tree, Node node)
        {
            var result = new Stack<Node>();
            FindNode(tree, node, result);
            var path = TransformPath(result);
            return path;
        }

        /// <summary>
        /// Stack nodes in path are in reverse order; transform them to an IEnumerable in top-down order
        /// </summary>
        private static IEnumerable<Node> TransformPath(Stack<Node> stack)
        {
            var path = new List<Node>();
            if (stack != null)
            {
                while (stack.Count > 0)
                {
                    path.Add(stack.Pop());
                }
            }
            return path;
        }

        /// <summary>
        /// Find a node in the tree, returning a stack of the path (in reverse order)
        /// </summary>
        private static bool FindNode(Node current, Node searchNode, Stack<Node> result)
        {
            if (current == null || searchNode == null || result == null)
            {
                return false;
            }

            result.Push(current);

            if (current == searchNode)
            {
                return true;
            }

            if (FindNode(current.Left, searchNode, result))
            {
                return true;
            }

            if (FindNode(current.Right, searchNode, result))
            {
                return true;
            }

            result.Pop();
            return false;
        }
    }
}
