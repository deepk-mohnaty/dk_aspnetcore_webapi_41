using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DK_DS_ALGO.DS
{

    public interface ITree
    {
        bool InsertNode(int NodeKey, string NodeValue);
        bool DeleteNode(int NodeKey, string NodeValue);
        TreeNode FindNode(int NodeKey, string NodeValue);
        ArrayList TraverseTreeValues();
    }

    public abstract class Tree
    {
        protected TreeNode _RootNode;
        public Tree()
        {

        }
        public int GetMax(int a, int b)
        {
            return (a > b) ? a : b;
        }
        public int GetHeight(TreeNode node)
        {
            if (node == null) return 0;
            return node.Height;
        }


    }
    public class AVLTree : Tree, ITree
    {
        public AVLTree()
        {

        }

        public int GetBalance(TreeNode node)
        {
            if (node == null) return 0;

            return GetHeight(node.LeftChildNode) - GetHeight(node.RightChildNode);
        }


        public bool InsertNode(int NodeKey, string NodeValue)
        {
            bool status = false;

            _RootNode = InsertNodeOpeartion(_RootNode, NodeKey, NodeValue);

            return status;
        }

        private TreeNode LeftRotate(TreeNode objNode)
        {
            TreeNode objRight = objNode.RightChildNode;
            TreeNode objRightLeft = objRight.LeftChildNode;

            objRight.LeftChildNode = objNode;
            objNode.RightChildNode = objRightLeft;

            objNode.Height = 1 + GetMax(GetHeight(objNode.LeftChildNode), GetHeight(objNode.RightChildNode));
            objRight.Height = 1 + GetMax(GetHeight(objRight.LeftChildNode), GetHeight(objRight.RightChildNode));

            return objRight;
        }

        private TreeNode RightRotate(TreeNode objNode)
        {
            TreeNode objLeft = objNode.LeftChildNode;
            TreeNode objLeftRight = objLeft.RightChildNode;

            objLeft.RightChildNode = objNode;
            objNode.LeftChildNode = objLeftRight;

            objNode.Height = 1 + GetMax(GetHeight(objNode.LeftChildNode), GetHeight(objNode.RightChildNode));
            objLeft.Height = 1 + GetMax(GetHeight(objLeft.LeftChildNode), GetHeight(objLeft.RightChildNode));

            return objLeft;
        }


        private TreeNode InsertNodeOpeartion(TreeNode objNode, int NodeKey, string NodeValue)
        {
            if (objNode == null) return new TreeNode(NodeKey, NodeValue);

            if (NodeKey > objNode.NodeKey)
            {
                objNode.RightChildNode = InsertNodeOpeartion(objNode.RightChildNode, NodeKey, NodeValue);
            }
            else if (NodeKey < objNode.NodeKey)
            {
                objNode.LeftChildNode = InsertNodeOpeartion(objNode.LeftChildNode, NodeKey, NodeValue);
            }
            else
            {
                return objNode;
            }

            objNode.Height = 1 + GetMax(GetHeight(objNode.LeftChildNode), GetHeight(objNode.RightChildNode));

            int balance = GetBalance(objNode);

            if(balance>1 && NodeKey < objNode.LeftChildNode.NodeKey)
            {
                return RightRotate(objNode);
            }
            if(balance<-1 && NodeKey > objNode.RightChildNode.NodeKey)
            {
                return LeftRotate(objNode);
            }

            if(balance>1 && NodeKey > objNode.LeftChildNode.NodeKey)
            {
                objNode.LeftChildNode = LeftRotate(objNode.LeftChildNode);
                return RightRotate(objNode);
            }

            if(balance < -1 && NodeKey < objNode.RightChildNode.NodeKey)
            {
                objNode.RightChildNode = RightRotate(objNode.RightChildNode);
                return LeftRotate(objNode);
            }


            return objNode;
        }

        public bool DeleteNode(int NodeKey, string NodeValue)
        {

        }
        public TreeNode FindNode(int NodeKey, string NodeValue)
        {

        }
        public ArrayList TraverseTreeValues()
        {

        }
    }


    public class TreeNode
    {
        public int NodeKey { get; set; }
        public string NodeValue { get; set; }
        public int Height { get; set; }
        public TreeNode LeftChildNode { get; set; }
        public TreeNode RightChildNode { get; set; }

        public TreeNode(int NodeKey, string NodeValue)
        {
            this.NodeKey = NodeKey;
            this.NodeValue = NodeValue;
            this.Height = 1;
        }

    }



}
