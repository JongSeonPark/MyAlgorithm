using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenGames.Algorithm.Tree
{
    /// <summary>
    /// 이진 탐색 트리 (BST)
    /// 중위순회법으로 제작되었음(in-order traversal)
    /// 왼 - 나 - 오 순으로 정렬을 뽑는다.
    /// 
    /// recursive 반복되는
    /// </summary>
    public class BinarySearchTree
    {
        public static void Test()
        {
            var bst = new BinarySearchTree();
            bst.Add(5, 3, 2, 1, 3, 10, 0, 5, 3);
            bst.PrintTree();
            var sort = bst.GetSortedData();
            sort.ForEach(d => Console.WriteLine(d));
        }

        TreeNode root;

        public void Add(int data)
        {
            if (root == null)
            {
                root = new TreeNode(data);
                return;
            }

            AddRecursive(root, data);
        }

        public void Add(params int[] data)
        {
            for (int i = 0; i < data.Length; i++)
                Add(data[i]);
        }

        void AddRecursive(TreeNode targetNode, int data)
        {
            if (data < targetNode.data)
            {
                if (targetNode.leftChild == null)
                    targetNode.leftChild = new TreeNode(data);
                else
                    AddRecursive(targetNode.leftChild, data);
            }
            else
            {
                if (targetNode.rightChild == null)
                    targetNode.rightChild = new TreeNode(data);
                else
                    AddRecursive(targetNode.rightChild, data);
            }
        }

        public List<int> GetSortedData()
        {
            void SortRecursive(List<int> list, TreeNode node)
            {
                if (node.leftChild != null) SortRecursive(list, node.leftChild);
                list.Add(node.data);
                if (node.rightChild != null) SortRecursive(list, node.rightChild);
            }

            List<int> result = new List<int>();
            SortRecursive(result, root);
            return result;
        }


        /// <summary>
        /// DFS 방식으로 작성
        /// </summary>
        public void PrintTree()
        {
            void PrintRecursive(TreeNode node, int depth)
            {
                var blank = new string(' ', depth * 4);
                Console.WriteLine($"{blank}{node.data}");
                if (node.leftChild != null) PrintRecursive(node.leftChild, depth + 1);
                if (node.rightChild != null) PrintRecursive(node.rightChild, depth + 1);
            }
            PrintRecursive(root, 0);
        }
    }

    internal class TreeNode
    {
        public int data;
        public TreeNode leftChild;
        public TreeNode rightChild;

        internal TreeNode(int data)
        {
            this.data = data;
        }
    }
   
}
